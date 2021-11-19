using Microsoft.EntityFrameworkCore;
using Model;
using Model.ViewModels;
using Persistence;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace Service
{
    public interface IDayService
    {
        IEnumerable<Day> GetAll();
        DayVM.ListItem GetListItem(int id);
        bool Add(Day model);
        bool Delete(int id);
        bool Update(Day model);
        DayVM.Update Get(int id);

    }

    public class DayService : IDayService
    {
        private readonly LastaContext _DayDbContext;

        public DayService(
            LastaContext DayDbContext
        )
        {
            _DayDbContext = DayDbContext;
        }

        public IEnumerable<Day> GetAll()

        {
            var result = new List<Day>();
            try
            {
                result = _DayDbContext.Day
                    .Include(x => x.DayCity)
                    .Include(x => x.DayCountry)
                    .ToList();
                foreach(var day in result)
                {
                    day.PhotoUrl = Helper.Helper.GetImageUrl(day.PhotoUrl);
                }
            }
            catch (System.Exception)
            {

            }

            return result;
        }

        public DayVM.Update Get(int id)
        {
            var result = new DayVM.Update();

            try
            {
                result = _DayDbContext.Day
                    .Include(x => x.DayCity)
                    .Include(x => x.DayCountry)
                    .Select(x => new DayVM.Update()
                    {
                        TourId = x.TourId,
                        DayId = x.DayId,
                        Name = x.Name,
                        PhotoUrl = Helper.Helper.GetImageUrl(x.PhotoUrl),
                        Description = x.Description,
                        IsBIncluded = x.IsBIncluded,
                        IsLIncluded = x.IsLIncluded,
                        IsDIncluded = x.IsDIncluded,
                        AccommodationId = x.AccommodationId
                        
                    })
                    .Single(x => x.DayId == id);

                result.Countries = new List<int>();
                result.Cities = new List<int>();


                foreach (var item in _DayDbContext.DayCountry.Where(x => x.DayId == id).ToList())
                    result.Countries.Add(item.CountryId);

                foreach (var item in _DayDbContext.DayCity.Where(x => x.DayId == id).ToList())
                    result.Cities.Add(item.CityId);

            }
            catch (System.Exception)
            {

            }

            return result;
        }

        public DayVM.ListItem GetListItem(int id)
        {
            var day = new DayVM.ListItem();

            try
            {
                day = _DayDbContext.Day
                    .Include(x => x.DayCity)
                    .Include(x => x.DayCountry)
                    .Select(x => new DayVM.ListItem()
                    {
                        TourId = x.TourId,
                        DayId = x.DayId,
                        Name = x.Name,
                        PhotoUrl = Helper.Helper.GetImageUrl(x.PhotoUrl),
                        Description = x.Description,
                        IsBIncluded = x.IsBIncluded,
                        IsLIncluded = x.IsLIncluded,
                        IsDIncluded = x.IsDIncluded,
                        ScheduleId = x.ScheduleId

                    })
                    .Single(x => x.DayId == id);

                day.Countries = new List<CountryVM.Info>();
                day.Cities = new List<CityVM.Info>();
                day.Accommodation = new AccommodationVM.Single();

                day.Accommodation = _DayDbContext.Day.Where(x => x.DayId == day.DayId)
                    .Include(x => x.Accommodation)
                    .Select(x => new AccommodationVM.Single
                    {
                        AccommodationId = x.Accommodation.AccommodationId,
                        Name = x.Accommodation.Name
                    }).FirstOrDefault();


                foreach (var item in _DayDbContext.DayCountry.Where(x => x.DayId == day.DayId).Select(x => x.Country).ToList())
                    day.Countries.Add(new CountryVM.Info
                    {
                        CountryId = item.CountryId,
                        Name = item.Name,
                        FlagUrl = Helper.Helper.GetImageUrl(item.FlagUrl)
                    });

                foreach (var item in _DayDbContext.DayCity.Where(x => x.DayId == day.DayId).Select(x => x.City).ToList())
                    day.Cities.Add(new CityVM.Info
                    {
                        CityId = item.CityId,
                        Name = item.Name,
                        PhotoUrl = Helper.Helper.GetImageUrl(item.PhotoUrl)
                    });

            }
            catch (System.Exception)
            {

            }

            return day;
        }

        public bool Add(Day model)
        {
            try
            {
                _DayDbContext.Add(model);
                _DayDbContext.SaveChanges();
            }
            catch (System.Exception)
            {
                return false;
            }

            return true;
        }

        public bool Update(Day model)
        {

            var originalModel = _DayDbContext.Day.Include(x=> x.DayCountry).Include(x=> x.DayCity).Single(x =>
                x.DayId == model.DayId
            );

            if (model.PhotoUrl != string.Empty)
                originalModel.PhotoUrl = model.PhotoUrl;

            _DayDbContext.DayCountry.RemoveRange(originalModel.DayCountry);
            _DayDbContext.DayCity.RemoveRange(originalModel.DayCity);
            _DayDbContext.SaveChanges();

            originalModel.AccommodationId = model.AccommodationId;
            originalModel.DayCity = model.DayCity;
            originalModel.DayCountry = model.DayCountry;
            originalModel.Description = model.Description;
            originalModel.Name = model.Name;
            originalModel.IsBIncluded = model.IsBIncluded;
            originalModel.IsLIncluded = model.IsLIncluded;
            originalModel.IsDIncluded = model.IsDIncluded;
           

         

            _DayDbContext.Day.Update(originalModel);
            _DayDbContext.SaveChanges();


            return true;
        }

        public bool Delete(int id)
        {
            try
            {

            }
            catch (System.Exception)
            {
                return false;
            }

            return true;
        }
    }
}
