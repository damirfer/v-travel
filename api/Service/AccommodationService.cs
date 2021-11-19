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
    public interface IAccommodationService
    {
        IEnumerable<AccommodationVM.List> GetAll();
        bool Add(Accommodation model);
        bool Delete(int id);
        bool Update(Accommodation model);
        AccommodationUpdateVM Get(int id);
        AccommodationUpdateVM.Mobile GetForMobile(int id);
        IEnumerable<Accommodation> GetByCities(int[] cityId);
        IEnumerable<AccommodationVM.List> GetByIndex(int index);
        IEnumerable<AccommodationVM.List> GetByName(string name);
        int GetCount();

    }

    public class AccommodationService : IAccommodationService
    {
        private readonly LastaContext _AccommodationDbContext;

        public AccommodationService(
            LastaContext AccommodationDbContext
        )
        {
            _AccommodationDbContext = AccommodationDbContext;
        }

        public IEnumerable<AccommodationVM.List> GetAll()

        {
            var result = new List<AccommodationVM.List>();
            try
            {
                result = _AccommodationDbContext.Accommodation
                    .Include(x => x.City)
                        .ThenInclude(y => y.Country)
                    .Include(x => x.AccommodationType)
                    .Select(x=>new AccommodationVM.List {
                        AccommodationId=x.AccommodationId,
                        City=x.City.Name,
                        Country=x.City.Country.Name,
                        Name=x.Name,
                        Type=x.AccommodationType.Name

                    })
                    .ToList();

            }
            catch (System.Exception)
            {

            }

            return result;
        }

        public AccommodationUpdateVM Get(int id)
        {
            var result = new AccommodationUpdateVM();

            try
            {
                result = _AccommodationDbContext.Accommodation.Include(x => x.AccommodationAmenity)
                    .Select(x => new AccommodationUpdateVM()
                    {
                        AccommodationId = x.AccommodationId,
                        Name = x.Name,
                        Description = x.Description,
                        Address = x.Address,
                        Telephone = x.Telephone,
                        WebsiteUrl = x.WebsiteUrl,
                        PhotoUrl = Helper.Helper.GetImageUrl(x.PhotoUrl),
                        CityId = x.CityId,
                        AccommodationTypeId = x.AccommodationTypeId,
                        AccommodationAmenity = new List<int>()

                    })
                    .Single(x => x.AccommodationId == id);
                result.AccommodationAmenity = new List<int>();
                foreach (var item in _AccommodationDbContext.AccommodationAmenity.Where(x => x.AccommodationId == id).ToList())
                    result.AccommodationAmenity.Add(item.AmenityId);

            }
            catch (System.Exception)
            {

            }

            return result;
        }

        public AccommodationUpdateVM.Mobile GetForMobile(int id)
        {
            var result = new AccommodationUpdateVM.Mobile();

            try
            {
                result = _AccommodationDbContext
                            .Accommodation
                            .Include(x => x.City)
                            .Include(x => x.AccommodationType)
                            .Select(x => new AccommodationUpdateVM.Mobile()
                            {
                                AccommodationId = x.AccommodationId,
                                Name = x.Name,
                                Description = x.Description,
                                Address = x.Address,
                                Telephone = x.Telephone,
                                WebsiteUrl = x.WebsiteUrl,
                                PhotoUrl = Helper.Helper.GetImageUrl(x.PhotoUrl),
                                CityName = x.City.Name,
                                AccommodationType = x.AccommodationType.Name,
                            })
                            .SingleOrDefault(x => x.AccommodationId == id);


                if(result != null)
                {
                    result.RoomAmenities = new List<AmenityVM>();
                    result.HotelAmenities = new List<AmenityVM>();
                    foreach (var item in _AccommodationDbContext.AccommodationAmenity.Where(x => x.AccommodationId == id).Select(x => x.Amenity).Include(x => x.AmenityType).ToList())
                    {
                        if (item.AmenityType.Name == "Hotel")
                        {
                            result.HotelAmenities.Add(new AmenityVM
                            {
                                AmenityId = item.AmenityId,
                                Name = item.Name
                            });
                        }
                        else
                        {
                            result.RoomAmenities.Add(new AmenityVM
                            {
                                AmenityId = item.AmenityId,
                                Name = item.Name
                            });
                        }
                    }
                }

            }
            catch (System.Exception e)
            {
                throw e;
            }

            return result;
        }

        public bool Add(Accommodation model)
        {
            try
            {
                _AccommodationDbContext.Add(model);
                _AccommodationDbContext.SaveChanges();
            }
            catch (System.Exception)
            {
                return false;
            }

            return true;
        }

        public bool Update(Accommodation model)
        {
            try
            {
                _AccommodationDbContext.AccommodationAmenity.RemoveRange(_AccommodationDbContext.AccommodationAmenity.Where(x => x.AccommodationId == model.AccommodationId).ToList());
                _AccommodationDbContext.SaveChanges();

                foreach (var item in model.AccommodationAmenity)
                {
                    item.AccommodationId = model.AccommodationId;
                    _AccommodationDbContext.AccommodationAmenity.Add(item);
                }


                _AccommodationDbContext.Accommodation.Update(model);
                _AccommodationDbContext.SaveChanges();
            }
            catch (System.Exception e)
            {
                return false;
            }

            return true;
        }

        public bool Delete(int id)
        {
            try
            {
                var Accommodation = _AccommodationDbContext.Accommodation.Find(id);
                _AccommodationDbContext.Entry(Accommodation).State = EntityState.Deleted;
                _AccommodationDbContext.SaveChanges();
            }
            catch (System.Exception)
            {
                return false;
            }

            return true;
        }

        public IEnumerable<Accommodation> GetByCities(int[] cityId)
        {
            var result = new List<Accommodation>();
            try
            {
                foreach (var id in cityId)
                {
                    var accommodations = _AccommodationDbContext.Accommodation.Where(x => x.CityId == id).ToList();
                    foreach (var accommodation in accommodations)
                    {
                        result.Add(accommodation);
                    }
                }
                 
            }
            catch (System.Exception)
            {

            }

            return result;
        }

        public IEnumerable<AccommodationVM.List> GetByIndex(int index)
        {
            var result = new List<AccommodationVM.List>();

            try
            {
                result = _AccommodationDbContext.Accommodation
                    .Include(x => x.AccommodationType)
                    .Include(x=>x.City).ThenInclude(y=>y.Country)
                    .Include(x=> x.AccommodationAmenity)
                    .Skip(index * 10).Take(10)
                    .Select(x => new AccommodationVM.List()
                    {
                        AccommodationId = x.AccommodationId,
                        City = x.City.Name,
                        Country = x.City.Country.Name,
                        Name=x.Name,
                        Type=x.AccommodationType.Name,
                    })
                    .ToList();
            }
            catch (System.Exception)
            {

            }

            return result;
        }

        public IEnumerable<AccommodationVM.List> GetByName(string name)
        {
            var result = new List<AccommodationVM.List>();

            try
            {
                result = _AccommodationDbContext
                           .Accommodation
                           .Include(x=>x.AccommodationType)
                           .Include(x => x.City).ThenInclude(x=>x.Country)
                          
                         .Where(x => x.Name.ToLower().StartsWith(name.ToLower()))
                         .Select(x => new AccommodationVM.List()
                         {
                             AccommodationId = x.AccommodationId,
                             Type=x.AccommodationType.Name,
                             City=x.City.Name,
                             Country = x.City.Country.Name,
                             Name=x.Name
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
                temp = _AccommodationDbContext.Accommodation.Count();
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
    }
}
