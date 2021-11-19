using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service;
using Model;
using Microsoft.AspNetCore.Authorization;

namespace api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class AmenityController : Controller
    {
        private readonly IAmenityService _AmenityService;

        public AmenityController(IAmenityService AmenityService)
        {
            _AmenityService = AmenityService;
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(
                _AmenityService.GetAll()
            );
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(
                _AmenityService.Get(id)
            );
        }

        [HttpGet]
        [Route("GetByAccommodation/{id}")]
        public IActionResult GetByAccommodation(int id)
        {
            return Ok(
                _AmenityService.GetByAccommodation(id)
                );
        }

        [HttpPost]
        public IActionResult Post([FromBody] Amenity model)
        {
            return Ok(_AmenityService.Add(model));
        }

        [HttpGet]
        [Route("GetByIndex/{index}/{typeId}")]
        public IActionResult GetByIndex(int index,int typeId)
        {
            return Ok(
                _AmenityService.GetByIndex(index,typeId)
            );
        }

        [HttpGet]
        [Route("GetByName/{name}/{typeId}")]
        public IActionResult GetByName(string name,int typeId)
        {
            return Ok(
                _AmenityService.GetByName(name,typeId)
            );
        }

        [HttpGet]
        [Route("GetCount/{typeId}")]
        public IActionResult GetCount(int typeId)
        {
            return Ok(
                _AmenityService.GetCount(typeId)
            );
        }


        [HttpPut]
        public IActionResult Update([FromBody] Amenity model)
        {
            return Ok(
                _AmenityService.Update(model)
                );
        }
    }
}