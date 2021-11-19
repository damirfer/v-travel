using Model;
using Persistence;
using System.Collections.Generic;
using System.Linq;


namespace Service
{
    public interface IPlaceTypeService
    {
        IEnumerable<PlaceType> GetAll();
        bool Add(PlaceType model);

    }

    public class PlaceTypeService : IPlaceTypeService
    {
        private readonly LastaContext _PlaceTypeDbContext;

        public PlaceTypeService(
            LastaContext PlaceTypeDbContext
        )
        {
            _PlaceTypeDbContext = PlaceTypeDbContext;
        }

        public IEnumerable<PlaceType> GetAll()
        {

            var result = new List<PlaceType>();

            try
            {
                result = _PlaceTypeDbContext.PlaceType.ToList();
            }
            catch (System.Exception)
            {

            }

            return result;
        }

        public bool Add(PlaceType model)
        {
            try
            {
                _PlaceTypeDbContext.PlaceType.Add(model);
                _PlaceTypeDbContext.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }
    }
}
