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
    [Produces("application/json")]
    [Route("[controller]")]
    public class LanguageController : Controller
    {
        private readonly ILanguageService _LanguageService;


        public LanguageController(ILanguageService LanguageService)
        {
            _LanguageService = LanguageService;
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(
                _LanguageService.GetAll()
            );
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(
                _LanguageService.Get(id)
            );
        }

        [HttpGet]
        [Route("GetByIndex/{index}")]
        public IActionResult GetByIndex(int index)
        {
            return Ok(
                _LanguageService.GetByIndex(index)
            );
        }

        [HttpGet]
        [Route("GetCount")]
        public IActionResult GetCount()
        {
            return Ok(
                _LanguageService.GetCount()
            );
        }

        [HttpGet]
        [Route("GetByName/{name}")]
        public IActionResult GetByIndexAndName(string name)
        {
            return Ok(
                _LanguageService.GetByName(name)
            );
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] Language model)
        {
            return Ok(
                _LanguageService.Add(model)
            );
        }

        // PUT api/values/5
        [HttpPut]
        public IActionResult Put([FromBody] Language model)
        {
            return Ok(
                _LanguageService.Update(model)
            );
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(
                _LanguageService.Delete(id)
            );
        }
    }
}