using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence;

namespace api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class SeedController : Controller
    {
        private LastaContext db;

        public SeedController(LastaContext db)
        {
            this.db = db;
        }

        [NonAction]
        public IActionResult Index()
        {
            if (!db.Country.Any())
                Persistence.DatabaseInitializer.SeedCountry(db);

            if (!db.City.Any())
                Persistence.DatabaseInitializer.SeedCity(db);

            if (!db.AccommodationType.Any())
                Persistence.DatabaseInitializer.SeedAccommodationType(db);
            
            if (!db.PlaceType.Any())
                Persistence.DatabaseInitializer.SeedPlaceType(db);

            if (!db.AmenityType.Any())
                Persistence.DatabaseInitializer.SeedAmenityType(db);

            if (!db.Amenity.Any())
                Persistence.DatabaseInitializer.SeedAmenity(db);

            if (!db.Language.Any())
                Persistence.DatabaseInitializer.SeedLanguages(db);

            if (!db.TransportType.Any())
                Persistence.DatabaseInitializer.SeedTransportTypes(db);



            return View();
        }
    }
}