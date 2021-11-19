using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Model;
using Service;

namespace api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class AccommodationController : Controller
    {
        private readonly IAccommodationService _AccommodationService;
        private readonly IHostingEnvironment _environment;


        public AccommodationController(IAccommodationService AccommodationService,
            IHostingEnvironment environment
            )
        {
            _AccommodationService = AccommodationService;
            _environment = environment;
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(
                _AccommodationService.GetAll()
            );
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(
                _AccommodationService.Get(id)
            );
        }

        [HttpGet("getForMobile/{id}")]
        public IActionResult GetForMobile(int id)
        {
            return Ok(
                _AccommodationService.GetForMobile(id)
            );
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post(Accommodation model)
        {
            // Uploading files
            model.PhotoUrl = UploadFiles();
            return Ok(
                _AccommodationService.Add(model)
            );
        }

        // PUT api/values/5
        [HttpPut]
        public IActionResult Put(Accommodation model)
        {
            // Uploading files
            var fileName = UploadFiles();
            if (string.IsNullOrEmpty(fileName))
            {
                Regex rgx = new Regex("[^/]+(?=/$|$)");
                Match match = rgx.Match(model.PhotoUrl);
                if (match.Success)
                {
                    model.PhotoUrl = match.Groups[0].Value;
                }

            }
            else
            {
                model.PhotoUrl = fileName;
            }
            return Ok(
                _AccommodationService.Update(model)
            );
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(
                _AccommodationService.Delete(id)
            );
        }

        [HttpGet]
        [Route("GetByCity")]
        public IActionResult GetByCity([FromQuery(Name = "id")]int[] id)
        {
            return Ok(
                _AccommodationService.GetByCities(id)
            );
        }
        [HttpGet]
        [Route("GetByIndex/{index}")]
        public IActionResult GetByIndex(int index)
        {
            return Ok(
                _AccommodationService.GetByIndex(index)
            );
        }

        [HttpGet]
        [Route("GetByName/{name}")]
        public IActionResult GetByName(string name)
        {
            return Ok(
                _AccommodationService.GetByName(name)
            );
        }

        [HttpGet]
        [Route("GetCount")]
        public IActionResult GetCount()
        {
            return Ok(
                _AccommodationService.GetCount()
            );
        }

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