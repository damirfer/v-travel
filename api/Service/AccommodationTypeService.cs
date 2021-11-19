using Microsoft.EntityFrameworkCore;
using Model;
using Persistence;
using System.Collections.Generic;
using System.Linq;


namespace Service
{
    public interface IAccommodationTypeService
    {
        IEnumerable<AccommodationType> GetAll();
        bool Add(AccommodationType model);

    }

    public class AccommodationTypeService : IAccommodationTypeService
    {
        private readonly LastaContext _AccommodationTypeDbContext;

        public AccommodationTypeService(
            LastaContext AccommodationTypeDbContext
        )
        {
            _AccommodationTypeDbContext = AccommodationTypeDbContext;
        }

        public IEnumerable<AccommodationType> GetAll()
        {

            var result = new List<AccommodationType>();

            try
            {
                result = _AccommodationTypeDbContext.AccommodationType.ToList();
            }
            catch (System.Exception)
            {

            }

            return result;
        }

        public bool Add(AccommodationType model)
        {
            try
            {
                _AccommodationTypeDbContext.AccommodationType.Add(model);
                _AccommodationTypeDbContext.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }
    }
}
