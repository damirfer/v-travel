using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Service;

namespace api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class TransportTypeController : Controller
    {
        private readonly ITransportTypeService _TransportTypeService;


        public TransportTypeController(ITransportTypeService TransportTypeService)
        {
            _TransportTypeService = TransportTypeService;
        }


        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(
                _TransportTypeService.GetAll()
            );
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(
                _TransportTypeService.Get(id)
            );
        }

        [HttpGet]
        [Route("GetByIndex/{index}")]
        public IActionResult GetByIndex(int index)
        {
            return Ok(
                _TransportTypeService.GetByIndex(index)
            );
        }

        [HttpGet]
        [Route("GetCount")]
        public IActionResult GetCount()
        {
            return Ok(
                _TransportTypeService.GetCount()
            );
        }

        [HttpGet]
        [Route("GetByName/{name}")]
        public IActionResult GetByIndexAndName(string name)
        {
            return Ok(
                _TransportTypeService.GetByName(name)
            );
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] TransportType model)
        {
            return Ok(
                _TransportTypeService.Add(model)
            );
        }

        // PUT api/values/5
        [HttpPut]
        public IActionResult Put([FromBody] TransportType model)
        {
            return Ok(
                _TransportTypeService.Update(model)
            );
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(
                _TransportTypeService.Delete(id)
            );
        }

    }
}