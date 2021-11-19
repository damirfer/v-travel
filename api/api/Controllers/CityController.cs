using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service;
using Model;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class CityController : Controller
    {
        private readonly ICityService _CityService;
        private readonly IHostingEnvironment _environment;


        public CityController(ICityService CityService,IHostingEnvironment environment)
        {
            _CityService = CityService;
            _environment = environment;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(
                _CityService.GetAll()
            );
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(
            _CityService.Get(id));
        }


        [HttpGet]
        [Route("GetByIndex/{index}")]
        public IActionResult GetByIndex(int index)
        {
            return Ok(
                _CityService.GetByIndex(index)
            );
        }

        [HttpGet]
        [Route("GetCount")]
        public IActionResult GetCount()
        {
            return Ok(
                _CityService.GetCount()
            );
        }

        [HttpGet]
        [Route("GetByName/{name}")]
        public IActionResult GetByIndexAndName(string name)
        {
            return Ok(
                _CityService.GetByName(name)
            );
        }


        [HttpPost]
        public IActionResult Add(City model)
        {
            var fileName = UploadFiles();
            model.PhotoUrl = fileName;
            return Ok(_CityService.Add(model));

        }

        [HttpPut]
        public IActionResult Update(City model)
        {
            var fileName = UploadFiles();
            model.PhotoUrl = fileName;
            return Ok(_CityService.Update(model));
        }

        [HttpGet]
        [Route("getbycountries")]
        public IActionResult GetByCountries([FromQuery(Name = "id")]int[] id)
        {

            return Ok(_CityService.GetByCountries(id));
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