using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Model;
using Persistence;
using System.Linq;
using Model.ViewModels;
using System.IO;

namespace Service
{
    public interface ICityService
    {
        IEnumerable<CityVM.List> GetAll();
        IEnumerable<CityVM.List> GetByIndex(int index);
        IEnumerable<CityVM.List> GetByName(string name);
        int GetCount();
        bool Add(City model);
        bool Delete(int id);
        bool Update(City model);
        City Get(int id);
        bool DeleteSection(int CityId, int sectionId);
        List<City> GetByCountries(int [] Countryids);

    }

    public class CityService : ICityService
    {
        private readonly LastaContext _CityDbContext;

        public CityService(
            LastaContext CityDbContext
        )
        {
            _CityDbContext = CityDbContext;
        }

        public IEnumerable<CityVM.List> GetAll()
        {
            var result = new List<CityVM.List>();

            try
            {
                result = _CityDbContext.City.Include(x => x.Country).Select(x=>new CityVM.List {

                    CityId=x.CityId,
                    Country=x.Country.Name,
                    Name=x.Name


                }).ToList();
            }
            catch (System.Exception)
            {

            }

            return result;
        }

        public IEnumerable<CityVM.List> GetByIndex(int index)
        {
            var result = new List<CityVM.List>();

            try
            {
                result = _CityDbContext.City
                    .Include(x=> x.Country)
                    .Skip(index * 10).Take(10)
                    .Select(x=> new CityVM.List()
                    {
                        CityId = x.CityId,
                        Country = x.Country.Name,
                        Name = x.Name
                    })
                    .ToList();
            }
            catch (System.Exception)
            {

            }

            return result;
        }

        public IEnumerable<CityVM.List> GetByName(string name)
        {
            var result = new List<CityVM.List>();

            try
            {
                result = _CityDbContext
                           .City
                           .Include(x=> x.Country)                 
                         .Where(x => x.Name.ToLower().StartsWith(name.ToLower()))
                         .Select(x=> new CityVM.List()
                         {
                             CityId = x.CityId,
                             Country = x.Country.Name,
                             Name = x.Name
                         })
                         .ToList();
            }
            catch (System.Exception)
            {

            }

            return result;
        }


        public City Get(int id)
        {
            var result = new City();

            try
            {
                result = _CityDbContext.City.Include(x => x.Sections).Single(x => x.CityId == id);
                result.PhotoUrl = Helper.Helper.GetImageUrl(result.PhotoUrl);

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
                temp = _CityDbContext.City.Count();
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

        public bool Add(City model)
        {
            try
            {
                _CityDbContext.Add(model);
                _CityDbContext.SaveChanges();
            }
            catch (System.Exception ex)
            {
                return false;
            }

            return true;
        }

        public bool Update(City model)
        {
            City OriginalModel = _CityDbContext.City.Where(x => x.CityId == model.CityId).FirstOrDefault();
            OriginalModel.Name = model.Name;
            OriginalModel.GeneralInfo = model.GeneralInfo;
            OriginalModel.CountryId = model.CountryId;
            OriginalModel.Latitude = model.Latitude;
            OriginalModel.Longitude = model.Longitude;

            if(model.PhotoUrl!=string.Empty)
                OriginalModel.PhotoUrl = model.PhotoUrl;

            var sections = _CityDbContext.CitySection.Where(x => x.CityId == OriginalModel.CityId).ToList();
           if (sections.Any())
            {
                _CityDbContext.CitySection.RemoveRange(sections);
            }
           foreach(var item in model.Sections)
            {
                _CityDbContext.CitySection.Add(new CitySection()
                {
                    CityId = OriginalModel.CityId,
                    Content = item.Content,
                    Title = item.Title
                });
            }
            _CityDbContext.City.Update(OriginalModel);
            _CityDbContext.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            try
            {
                var City = _CityDbContext.City.Find(id);
                _CityDbContext.Entry(City).State = EntityState.Deleted;
                _CityDbContext.CitySection.RemoveRange(City.Sections.ToList());
                _CityDbContext.SaveChanges();
            }
            catch (System.Exception)
            {
                return false;
            }

            return true;
        }

        public bool DeleteSection(int CityId, int sectionId)
        {
            try
            {
                _CityDbContext.City.Where(x => x.CityId == CityId).First().Sections.RemoveAt(sectionId);
                _CityDbContext.Entry(new CitySection { CitySectionId = sectionId }).State = EntityState.Deleted;
                _CityDbContext.SaveChanges();
            }
            catch (System.Exception)
            {
                return false;
            }

            return true;
        }

        public List<City> GetByCountries(int[] Countryids)
        {
            List<City> temp = new List<City>();


            foreach (var item in Countryids)
            {
                var gradovi = _CityDbContext.City.Where(x => x.CountryId == item).ToList();

                foreach (var c in gradovi)
                {
                    temp.Add(c);
                }

            }
            return temp;




        }
    }
}
