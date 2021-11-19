using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using Service;

namespace api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class AccommodationTypeController : Controller
    {
        private readonly IAccommodationTypeService _AccommodationTypeService;

        public AccommodationTypeController(IAccommodationTypeService AccommodationTypeService)
        {
            _AccommodationTypeService = AccommodationTypeService;
        }



        [HttpGet]
        public IActionResult Get()
        {
            return Ok(
                _AccommodationTypeService.GetAll()
            );
        }
        [HttpPost]
        public IActionResult Add([FromBody] AccommodationType model)
        {
            return Ok(
                _AccommodationTypeService.Add(model));
        }
    }
}