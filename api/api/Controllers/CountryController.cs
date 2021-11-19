using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service;
using Model;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using System.Text.RegularExpressions;

namespace api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class CountryController : Controller
    {
        private readonly ICountryService _CountryService;
        private readonly IHostingEnvironment _environment;

        public CountryController(ICountryService CountryService, IHostingEnvironment environment)
        {
            _CountryService = CountryService;
            _environment = environment;
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(
                _CountryService.GetAll()
            );
        }

        [HttpGet]
        [Route("GetByIndex/{index}")]
        public IActionResult GetByIndex(int index)
        {
            return Ok(
                _CountryService.GetByIndex(index)
            );
        }

        [HttpGet]
        [Route("GetInfo/{countryId}")]
        public IActionResult GetInfo(int countryId)
        {
            return Ok(
                _CountryService.GetInfo(countryId)
            );
        }

        [HttpGet]
        [Route("GetCount")]
        public IActionResult GetCount()
        {
            return Ok(
                _CountryService.GetCount()
            );
        }
        [HttpGet]
        [Route("GetByName/{name}")]
        public IActionResult GetByIndexAndName(string name)
        {
            return Ok(
                _CountryService.GetByName(name)
            );
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(
                _CountryService.Get(id)
            );
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post(Country model)
        {
            // Uploading files
            var fileName = UploadFiles();
            if (string.IsNullOrEmpty(fileName))
            {
                Regex rgx = new Regex("[^/]+(?=/$|$)");
                Match match = rgx.Match(model.FlagUrl);
                if (match.Success)
                {
                    model.FlagUrl= match.Groups[0].Value;
                }

            }
            else
            {
                model.FlagUrl = fileName;
            }
            return Ok(
                _CountryService.Add(model)
            );
        }

        // PUT api/values/5
        [HttpPut]
        public IActionResult Put(Country model)
        {
            var fileName = UploadFiles();
            model.FlagUrl = fileName;
            return Ok(
                _CountryService.Update(model)
            );
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(
                _CountryService.Delete(id)
            );
        }

        [HttpDelete("{countryId}/{sectionId}")]
        public IActionResult DeleteSection(int countryId, int sectionId)
        {
            return Ok(
                _CountryService.DeleteSection(countryId, sectionId)
            );
        }


        // GET api/values/5
        [HttpGet("GetByTour/{id}")]
        public IActionResult GetByTour(int id)
        {
            return Ok(
                _CountryService.GetByTour(id)
            );
        }

        [NonAction]
        private string UploadFiles()
        {
            var uploadsRootFolder = Path.Combine(_environment.WebRootPath, "images");
            if (!Directory.Exists(uploadsRootFolder))
            {
                Directory.CreateDirectory(uploadsRootFolder);
            }
            var files = Request.Form.Files;
            foreach (var file in files)

            {
                if (file == null || file.Length == 0)
                {
                    continue;
                }

                var filePath = Path.Combine(uploadsRootFolder, file.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                    return file.FileName;
                }
            }

            return string.Empty;
        }


    }
}
