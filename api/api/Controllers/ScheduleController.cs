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
    public class ScheduleController : Controller
    {
        private readonly IScheduleService _ScheduleService;

        public ScheduleController(IScheduleService ScheduleService)
        {
            _ScheduleService = ScheduleService;
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(
                _ScheduleService.GetAll()
            );
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(
                _ScheduleService.Get(id)
            );
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] Schedule model)
        {
            return Ok(
                _ScheduleService.Add(model)
            );
        }

        // PUT api/values/5
        [HttpPut]
        public IActionResult Put([FromBody] Schedule model)
        {
            
            return Ok(
                _ScheduleService.Update(model)
            );
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(
                _ScheduleService.Delete(id)
            );
        }


        // GET api/values/5
        [HttpGet("GetByTour/{id}")]
        public IActionResult GetByTour(int id)
        {
            return Ok(
                _ScheduleService.GetByTour(id)
            );
        }

    }
}
