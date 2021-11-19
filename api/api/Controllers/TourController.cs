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
    public class TourController : Controller
    {
        private readonly ITourService _TourService;
        private readonly IHostingEnvironment _environment;

        public TourController(ITourService TourService, IHostingEnvironment environment)
        {
            _TourService = TourService;
            _environment = environment;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(
                _TourService.GetAll()
            );
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(
                _TourService.Get(id)
            );
        }

        [HttpGet]
        [Route("GetByIndex/{index}")]
        public IActionResult GetByIndex(int index)
        {
            return Ok(
                _TourService.GetByIndex(index)
            );
        }

        [HttpGet]
        [Route("GetCount")]
        public IActionResult GetCount()
        {
            return Ok(
                _TourService.GetCount()
            );
        }

        [HttpGet]
        [Route("GetByName/{name}")]
        public IActionResult GetByIndexAndName(string name)
        {
            return Ok(
                _TourService.GetByName(name)
            );
        }

        [HttpPost]
        public IActionResult Post(Tour model)
        {
            model.PhotoUrl = UploadFiles();
            return Ok(
                _TourService.Add(model)
            );
        }

        [HttpPut]
        public IActionResult Put(Tour model)
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
                _TourService.Update(model)
            );
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(
                _TourService.Delete(id)
            );
        }

        [HttpGet]
        [Route("ByBookingForMobile/{bookingId}")]
        public IActionResult ByBookingForMobile(int bookingId)
        {
            return Ok(
                _TourService.ByBookingForMobile(bookingId)
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

        [HttpGet]
        [Route("getinfo/{bookingId}")]
        public IActionResult GetInfo(int bookingId)
        {
            return Ok(
                _TourService.GetInfo(bookingId));
        }

    }
}