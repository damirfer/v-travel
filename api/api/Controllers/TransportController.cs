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
    public class TransportController : Controller
    {
        private readonly IHostingEnvironment _environment;
        private readonly ITransportService _transportService;
        public TransportController(ITransportService transportService, IHostingEnvironment environment)
        {
            _transportService = transportService;
            _environment = environment;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(
                _transportService.GetAll()
            );
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(
                _transportService.Get(id)
            );
        }

        [HttpPost]
        public IActionResult Post(Transport model)
        {
            var fileName = UploadFiles();
            model.PhotoUrl = fileName;
            return Ok(
                _transportService.Add(model)
            );
        }

        [HttpPut]
        public IActionResult Put(Transport model)
        {
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
                _transportService.Update(model)
            );
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(
                _transportService.Delete(id)
            );
        }

        [HttpGet]
        [Route("GetByCity")]
        public IActionResult GetByCity([FromQuery(Name = "id")]int[] id)
        {
            return Ok(
                _transportService.GetByCities(id)
            );
        }
        [HttpGet]
        [Route("GetByIndex/{index}")]
        public IActionResult GetByIndex(int index)
        {
            return Ok(
                _transportService.GetByIndex(index)
            );
        }

        [HttpGet]
        [Route("GetByName/{name}")]
        public IActionResult GetByName(string name)
        {
            return Ok(
                _transportService.GetByName(name)
            );
        }

        [HttpGet]
        [Route("GetCount")]
        public IActionResult GetCount()
        {
            return Ok(
                _transportService.GetCount()
            );
        }

        [HttpGet]
        [Route("GetTransportType")]
        public IActionResult GetTransportType()
        {
            return Ok(
                _transportService.GetTransportTypes()
            );
        }

        [HttpGet]
        [Route("GetByCityAndType/{transportTypeId}/{cityId}")]
        public IActionResult GetByCityAndType(int transportTypeId, int cityId)
        {
            return Ok(
                _transportService.GetByCityAndType(transportTypeId, cityId)
            );
        }
        [HttpGet]
        [Route("GetByTour/{tourId}")]
        public IActionResult GetByTour(int tourId)
        {
            return Ok(
                _transportService.GetByTour(tourId)
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