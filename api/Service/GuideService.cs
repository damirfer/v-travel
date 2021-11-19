using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Model;
using Persistence;
using System.Linq;
using Model.ViewModels;
using System.IO;
using AutoMapper;

namespace Service
{
    public interface IGuideService
    {
        IEnumerable<GuideVM.List> GetAll();
        IEnumerable<GuideVM.List> GetByIndex(int index);
        IEnumerable<GuideVM.List> GetByName(string name);
        int GetCount();
        bool Add(Guide model);
        bool Delete(int id);
        bool Update(Guide model);
        GuideVM.Single Get(int id);


    }

    public class GuideService : IGuideService
    {
        private readonly LastaContext _GuideService;
        private readonly IMapper _mapper;

        public GuideService(
            LastaContext GuideDbContext,
            IMapper mapper
        )
        {
            _GuideService = GuideDbContext;
            _mapper = mapper;

        }

        public IEnumerable<GuideVM.List> GetAll()
        {
            var result = new List<GuideVM.List>();

            try
            {
                result = _GuideService.Guide.Select(x=>new GuideVM.List {
                    
                    FirstName=x.FirstName,
                    Bio=x.Bio,
                    LastName=x.LastName,
                    GuideId=x.GuideId,
                    PhotoUrl=x.PhotoUrl


                    

                }).ToList();
            }
            catch (System.Exception)
            {

            }

            return result;
        }

        public IEnumerable<GuideVM.List> GetByIndex(int index)
        {
            var result = new List<GuideVM.List>();

            try
            {
                result = _GuideService.Guide
                    .Skip(index * 10).Take(10)
                    .Select(x=> new GuideVM.List()
                    {

                        FirstName = x.FirstName,
                        Bio = x.Bio,
                        LastName = x.LastName,
                        GuideId = x.GuideId,
                        PhotoUrl = x.PhotoUrl

                    })
                    .ToList();
            }
            catch (System.Exception error )
            {

            }

            return result;
        }

        public IEnumerable<GuideVM.List> GetByName(string name)
        {
            var result = new List<GuideVM.List>();

            try
            {
                result = _GuideService.Guide
                         .Where(x => x.FirstName.ToLower().StartsWith(name.ToLower()) || x.LastName.ToLower().StartsWith(name.ToLower())  )
                         .Select(x=> new GuideVM.List()
                         {
                             FirstName = x.FirstName,
                             Bio = x.Bio,
                             LastName = x.LastName,
                             GuideId = x.GuideId,
                             PhotoUrl = x.PhotoUrl
                         })
                         .ToList();
            }
            catch (System.Exception)
            {

            }

            return result;
        }


        public GuideVM.Single Get(int id)
        {
            var result = new GuideVM.Single();

            try
            {

                var model = _GuideService.Guide.Single(x => x.GuideId == id);

                _mapper.Map(model, result);
                result.PhotoUrl = Helper.Helper.GetImageUrl(model.PhotoUrl);

                result.GuideLanguage = new List<int>();
                foreach (var item in _GuideService.GuideLanguage.Where(x => x.GuideId == id).ToList())
                    result.GuideLanguage.Add(item.LanguageId);

            }
            catch (System.Exception)
            {

            }

            return result;
        }

        public int GetCount()
        {
            decimal temp = 0;

            try
            {
                temp = _GuideService.City.Count();
            }
            catch (System.Exception)
            {

            }
            temp /= 10;
            //Gets decimal of number - if number is 10.45 dec = 0.45
            var dec = temp - Math.Truncate(temp);

            int result = (int)temp;

            if (dec != 0)
                result++;

            return result;
        }

        public bool Add(Guide model)
        {
            try
            {
                model.PasswordSalt = Helper.Helper.GenerateSalt();
                model.PasswordHash = Helper.Helper.GenerateHash(model.PasswordSalt,model.PasswordHash);
                _GuideService.Guide.Add(model);
                _GuideService.SaveChanges();
            }
            catch (System.Exception)
            {
                return false;
            }

            return true;
        }

        public bool Update(Guide modelDTO)
        {
            try
            {
                _GuideService.GuideLanguage.RemoveRange(_GuideService.GuideLanguage.Where(x => x.GuideId == modelDTO.GuideId).ToList());
                _GuideService.SaveChanges();

                foreach (var language in modelDTO.GuideLanguage)
                {
                    language.GuideId = modelDTO.GuideId;
                    _GuideService.GuideLanguage.Add(language);
                }

                var model = _GuideService.Guide.Single(x => x.GuideId == modelDTO.GuideId);
                model.FirstName = modelDTO.FirstName;
                model.LastName = modelDTO.LastName;
                model.Bio = modelDTO.Bio;
                model.PhotoUrl = modelDTO.PhotoUrl;

                _GuideService.Guide.Update(model);
                _GuideService.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;

            }

        }

        public bool Delete(int id)
        {
            try
            {
                var Guide = _GuideService.Guide.Find(id);
                _GuideService.Entry(Guide).State = EntityState.Deleted;
               
                _GuideService.SaveChanges();
            }
            catch (System.Exception)
            {
                return false;
            }

            return true;
        }

    }
}
