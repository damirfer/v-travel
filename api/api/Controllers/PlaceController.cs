using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Service;

namespace api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class PlaceController : Controller
    {
        private readonly IPlaceService _placeService;
        private readonly IHostingEnvironment _environment;
        public PlaceController(IPlaceService placeService, IHostingEnvironment environment)
        {
            _placeService = placeService;
            _environment = environment;

        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(
                _placeService.GetAll()
            );
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(
                 _placeService.Get(id)
            );
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post(Place model)
        {
            var fileName = UploadFiles();
            model.PhotoUrl = fileName;
            return Ok(
                 _placeService.Add(model)
            );
        }

        // PUT api/values/5
        [HttpPut]
        public IActionResult Put(Place model)
        {
            var fileName = UploadFiles();
            if(string.IsNullOrEmpty(fileName))
            {
                Regex rgx = new Regex("[^/]+(?=/$|$)");
                Match match = rgx.Match(model.PhotoUrl);
                if(match.Success)
                {
                    model.PhotoUrl = match.Groups[0].Value;
                }

            }else
            {
                model.PhotoUrl = fileName;
            }
            return Ok(
                 _placeService.Update(model)
            );
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(
                 _placeService.Delete(id)
            );
        }

       

        [HttpGet]
        [Route("GetByCity")]
        public IActionResult GetByCity([FromQuery(Name = "id")]int[] id)
        {
            return Ok(
                 _placeService.GetByCities(id)
            );
        }
        [HttpGet]
        [Route("GetByIndex/{index}")]
        public IActionResult GetByIndex(int index)
        {
            return Ok(
                 _placeService.GetByIndex(index)
            );
        }

        [HttpGet]
        [Route("GetByName/{name}")]
        public IActionResult GetByName(string name)
        {
            return Ok(
                 _placeService.GetByName(name)
            );
        }

        [HttpGet]
        [Route("GetCount")]
        public IActionResult GetCount()
        {
            return Ok(
                 _placeService.GetCount()
            );
        }

        [HttpGet]
        [Route("GetByType/{id}")]
        public IActionResult GetByType(int id)
        {
            return Ok(
                 _placeService.GetByType(id)
            );
        }

        [HttpGet]
        [Route("GetTypes")]
        public IActionResult GetTypes()
        {
            return Ok(
                 _placeService.GetTypes()
            );
        }
        [HttpGet]
        [Route("GetByTour/{tourId}")]
        public IActionResult GetByTour(int tourId)
        {
            return Ok(
                _placeService.GetByTour(tourId)
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