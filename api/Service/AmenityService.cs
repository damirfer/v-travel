using Microsoft.EntityFrameworkCore;
using Model;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public interface IAmenityService
    {
        IEnumerable<Amenity> GetAll();
        IEnumerable<Amenity> GetByAccommodation(int AccommodationId);
        bool Add(Amenity model);
        Amenity Get(int id);
        bool Update(Amenity model);
        IEnumerable<Amenity> GetByIndex(int index, int typeId);
        IEnumerable<Amenity> GetByName(string name, int typeId);
        IEnumerable<Amenity> GetByType(int typeId);
        int GetCount(int typeId);

    }
    public class AmenityService : IAmenityService
    {
        private readonly LastaContext _AmenityDbContext;

        public AmenityService(
            LastaContext AmenityDbContext
        )
        {
            _AmenityDbContext = AmenityDbContext;
        }

        public Amenity Get(int id)
        {
            Amenity result = new Amenity();
            try
            {
                result = _AmenityDbContext.Amenity.Where(x => x.AmenityId == id).FirstOrDefault();
            }catch(System.Exception ex)
            {

            }
            return result;
        }

        public bool Add(Amenity model)
        {
            try
            {
                _AmenityDbContext.Amenity.Add(model);
                _AmenityDbContext.SaveChanges();
            }
            catch (System.Exception ex)
            {
                return false;
            }

            return true;
        }

        public IEnumerable<Amenity> GetAll()
        {
            var result = new List<Amenity>();

            try
            {
                result = _AmenityDbContext.Amenity.Include(x=> x.AmenityType).ToList();
            }
            catch (System.Exception)
            {

            }

            return result;
        }

        public bool Update(Amenity model)
        {
            try
            {
                _AmenityDbContext.Amenity.Update(model);
                _AmenityDbContext.SaveChanges();
            }
            catch(System.Exception ex)
            {
                return false;
            }

            return true;
        }

        public IEnumerable<Amenity> GetByIndex(int index,int typeId)
        {
            var result = new List<Amenity>();

            try
            {
                result = _AmenityDbContext.Amenity.Where(x => x.AmenityTypeId == typeId).Skip(index * 10).Take(10).ToList();
            }
            catch (System.Exception error)
            {

            }

            return result;
        }

        public IEnumerable<Amenity> GetByName(string name,int typeId)
        {
            var result = new List<Amenity>();

            try
            {
                result = _AmenityDbContext.Amenity
                         .Where(x => x.Name.ToLower().StartsWith(name.ToLower()))
                         .Select(x => new Amenity()
                         {
                             Name=x.Name,
                             AmenityId = x.AmenityId,
                         })
                         .ToList();
            }
            catch (System.Exception)
            {

            }

            return result;
        }

        public IEnumerable<Amenity> GetByAccommodation(int AccommodationId)
        {
            IEnumerable<Amenity> AmenityList = _AmenityDbContext.AccommodationAmenity.Where(x => x.AccommodationId == AccommodationId).Select(x => x.Amenity).ToList();
            return AmenityList;
        }

        public IEnumerable<Amenity> GetByType(int typeId)
        {
            var result = new List<Amenity>();

            try
            {
                result = _AmenityDbContext.Amenity
                         .Where(x => x.AmenityTypeId==typeId)
                         .Select(x => new Amenity()
                         {
                             Name=x.Name,
                             AmenityId = x.AmenityId,
                         })
                         .ToList();
            }
            catch (System.Exception)
            {

            }

            return result;
        }

        public int GetCount(int typeId)
        {
            decimal temp = 0;

            try
            {
                temp = _AmenityDbContext.Amenity.Where(x=> x.AmenityTypeId==typeId).Count();
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
