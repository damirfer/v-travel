using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.ViewModels;
using Service;

namespace api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class TravelerController : Controller
    {
        private ITravelerService _TravelerService;
        public TravelerController(ITravelerService travelerService)
        {
            _TravelerService = travelerService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(
                _TravelerService.GetAll()
            );
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(
            _TravelerService.Get(id));
        }

        [HttpGet]
        [Route("GetByBooking/{index}")]
        public IActionResult GetByBooking(int index)
        {
            return Ok(
                _TravelerService.GetByBooking(index)
            );
            
        }

        [HttpPost]
        public IActionResult Add([FromBody]Traveler model)
        {

            return Ok(_TravelerService.Add(model));

        }

        [HttpPut]
        public IActionResult Update([FromBody]Traveler model)
        {
            return Ok(_TravelerService.Update(model));
        }

        [HttpPost]
        [Route("Delete")]
        public IActionResult Delete([FromBody]TravelerVM.DeleteModel vm)
        {
            return Ok(
                _TravelerService.Delete(vm)
            );

        }

    }
}