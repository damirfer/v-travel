using Microsoft.EntityFrameworkCore;
using Model;
using Model.ViewModels;
using Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Service
{

    public interface IPlaceService

    {
        IEnumerable<PlaceVM.List> GetAll();
        bool Add(Place model);
        bool Delete(int id);
        bool Update(Place model);
        PlaceVM.Single Get(int id);
        IEnumerable<Place> GetByCities(int[] cityId);
        IEnumerable<PlaceVM.List> GetByIndex(int index);
        IEnumerable<PlaceVM.List> GetByName(string name);
        int GetCount();
        List<Place> GetByType(int id);
        List<PlaceType> GetTypes();
        IEnumerable<TourPlaceVM.Single> GetByTour(int tourId);
    }
    public class PlaceService : IPlaceService
    {
        private readonly LastaContext _PlaceContext;

        public PlaceService(LastaContext PlaceDbContext)
        {
            _PlaceContext = PlaceDbContext;
        }

        public IEnumerable<PlaceVM.List> GetAll()

        {
            var result = new List<PlaceVM.List>();
            try
            {
                result = _PlaceContext.Place
                    .Include(x => x.City)
                    .Include(x => x.PlaceType)

                    .Select(x => new PlaceVM.List
                    {
                        PlaceId = x.PlaceId,
                        CityName = x.City.Name,
                        Description = x.Description,
                        PhotoUrl = x.PhotoUrl,
                        WorkingHours = x.WorkingHours,
                        Name = x.Name,
                        Type = x.PlaceType.PlaceTypeName



                    })
                    .ToList();

            }
            catch (System.Exception)
            {

            }

            return result;
        }

        public PlaceVM.Single Get(int id)
        {
            var result = new PlaceVM.Single();

            try
            {
                result = _PlaceContext.Place.Where(x => x.PlaceId == id)
                    .Select(x => new PlaceVM.Single()
                    {
                        Name = x.Name,
                        PhotoUrl = Helper.Helper.GetImageUrl(x.PhotoUrl),
                        Description = x.Description,
                        PlaceId = x.PlaceId,
                        WorkingHours = x.WorkingHours,
                        CityId = x.CityId,
                        PlaceTypeId = x.PlaceTypeId,
                        CountryId = x.City.CountryId
                    })
                    .FirstOrDefault();



            }
            catch (System.Exception e)
            {
                throw e;
            }

            return result;
        }

        public bool Add(Place model)
        {
            try
            {
                _PlaceContext.Add(model);
                _PlaceContext.SaveChanges();
            }
            catch (System.Exception e)
            {
                throw e;
            }

            return true;
        }

        public bool Update(Place model)
        {
            try
            {

                _PlaceContext.Place.Update(model);
                _PlaceContext.SaveChanges();
            }
            catch (System.Exception e)
            {
                throw e;
            }

            return true;
        }

        public bool Delete(int id)
        {
            try
            {
                var Place = _PlaceContext.Place.Find(id);
                _PlaceContext.Entry(Place).State = EntityState.Deleted;

                _PlaceContext.SaveChanges();
            }
            catch (System.Exception)
            {
                return false;
            }

            return true;
        }



        public IEnumerable<Place> GetByCities(int[] cityId)
        {
            var result = new List<Place>();
            try
            {
                foreach (var id in cityId)
                {
                    var Places = _PlaceContext.Place.Where(x => x.CityId == id).ToList();
                    foreach (var Place in Places)
                    {
                        result.Add(Place);
                    }
                }

            }
            catch (System.Exception)
            {

            }

            return result;
        }

        public IEnumerable<PlaceVM.List> GetByIndex(int index)
        {
            var result = new List<PlaceVM.List>();

            try
            {
                result = _PlaceContext.Place

                    .Include(x => x.City)
                    .Skip(index * 10).Take(10)
                    .Select(x => new PlaceVM.List()
                    {

                        Name = x.Name,
                        PhotoUrl = x.PhotoUrl,
                        CityName = x.City.Name,
                        Description = x.Description,
                        PlaceId = x.PlaceId,
                        WorkingHours = x.WorkingHours,
                        Type = x.PlaceType.PlaceTypeName


                    })
                    .ToList();
            }
            catch (System.Exception)
            {

            }

            return result;
        }

        public IEnumerable<PlaceVM.List> GetByName(string name)
        {
            var result = new List<PlaceVM.List>();

            try
            {
                result = _PlaceContext
                           .Place

                           .Include(x => x.City)

                         .Where(x => x.Name.ToLower().StartsWith(name.ToLower()))
                         .Select(x => new PlaceVM.List()
                         {
                             Name = x.Name,
                             PhotoUrl = x.PhotoUrl,
                             CityName = x.City.Name,
                             Description = x.Description,
                             PlaceId = x.PlaceId,
                             WorkingHours = x.WorkingHours,
                             Type = x.PlaceType.PlaceTypeName

                         })
                         .ToList();
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
                temp = _PlaceContext.Place.Count();
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

        public List<Place> GetByType(int id)
        {
            List<Place> list = _PlaceContext.Place.Where(x => x.PlaceTypeId == id).ToList();
            return list;

        }

        public IEnumerable<TourPlaceVM.Single> GetByTour(int tourId)
        {
            List<TourPlaceVM.Single> res = new List<TourPlaceVM.Single>();


            List<City> cities = _PlaceContext.DayCity.Where(dc => dc.Day.TourId == tourId).Select(dc => dc.City).Distinct().ToList();

            foreach (var city in cities)
            {
                List<TourPlaceVM.List> CurrentPlaces = _PlaceContext.Place.Where(p => p.CityId == city.CityId)
                                                                .Select(t => new TourPlaceVM.List
                                                                {
                                                                    Description = t.Description,
                                                                    Name = t.Name,
                                                                    PhotoUrl = Helper.Helper.GetImageUrl(t.PhotoUrl),
                                                                    PlaceId = t.PlaceId,
                                                                    Type = t.PlaceType.PlaceTypeName,
                                                                    WorkingHours = t.WorkingHours
                                                                }).ToList();
                res.Add(new TourPlaceVM.Single
                {
                    CityName = city.Name,
                    PlaceList = CurrentPlaces
                });

            }

            return res;
        }

        public List<PlaceType> GetTypes()
        {
            return _PlaceContext.PlaceType.ToList();
        }
    }
}
