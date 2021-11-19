using System;
using Microsoft.AspNetCore.Mvc;
using Service;
using Model;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class BookingController : Controller
    {
        private readonly IBookingService _BookingService;
        private readonly IHostingEnvironment _environment;

        public BookingController(IBookingService BookingService, IHostingEnvironment environment)
        {
            _BookingService = BookingService;
            _environment = environment;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(
                _BookingService.GetAll()
            );
        }

        [HttpGet]
        [Route("GetByIndex/{index}")]
        public IActionResult GetByIndex(int index)
        {
            return Ok(
                _BookingService.GetByIndex(index)
            );
        }

        [HttpGet]
        [Route("GetCount")]
        public IActionResult GetCount()
        {
            return Ok(
                _BookingService.GetCount()
            );
        }
        [HttpGet]
        [Route("GetByName/{name}")]
        public IActionResult GetByIndexAndName(string name)
        {
            return Ok(
                _BookingService.GetByName(name)
            );
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(
                _BookingService.Get(id)
            );
        }

        [HttpPost]
        public IActionResult Post([FromBody]Booking model)
        {
            return Ok(
                _BookingService.Add(model)
            );
        }

        [HttpPut]
        public IActionResult Put([FromBody]Booking model)
        {
            return Ok(
                _BookingService.Update(model)
            );
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(
                _BookingService.Delete(id)
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
        [Route("GetData")]
        public IActionResult GetData()
        {

            var travelerId =  Int32.Parse(User.FindFirst(ClaimTypes.Sid).Value);

            return Ok(
                        _BookingService.GetData(travelerId)
                    );
        }

        [HttpGet]
        [Route("GetCitiesByDay/{bookingId}")]
        public IActionResult GetCitiesByDay(int bookingId)
        {
            return Ok(
                _BookingService.GetCitiesByDay(bookingId)
            );
        }

    }
}
