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
    public class PlaceTypeController : Controller
    {
        private readonly IPlaceTypeService _PlaceTypeService;

        public PlaceTypeController(IPlaceTypeService PlaceTypeService)
        {
            _PlaceTypeService = PlaceTypeService;
        }



        [HttpGet]
        public IActionResult Get()
        {
            return Ok(
                _PlaceTypeService.GetAll()
            );
        }
        [HttpPost]
        public IActionResult Add([FromBody] PlaceType model)
        {
            return Ok(
                _PlaceTypeService.Add(model));
        }
    }
}