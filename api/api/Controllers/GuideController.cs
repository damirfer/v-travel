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
    public class GuideController : Controller
    {
        private readonly IHostingEnvironment _environment;
        private readonly IGuideService _GuideService;

        public GuideController(IGuideService GuideService, IHostingEnvironment environment)
        {
            _GuideService = GuideService;
            _environment = environment;
        }
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(
                _GuideService.GetAll()
            );
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(
                _GuideService.Get(id)
            );
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post(Guide model)
        {
            var fileName = UploadFiles();
            model.PhotoUrl = fileName;
            return Ok(
                _GuideService.Add(model)
            );
        }

        // PUT api/values/5
        [HttpPut]
        public IActionResult Put(Guide model)
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
                _GuideService.Update(model)
            );
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(
                _GuideService.Delete(id)
            );
        }

        

      
        [HttpGet]
        [Route("GetByIndex/{index}")]
        public IActionResult GetByIndex(int index)
        {
            return Ok(
                _GuideService.GetByIndex(index)
            );
        }

        [HttpGet]
        [Route("GetByName/{name}")]
        public IActionResult GetByName(string name)
        {
            return Ok(
                _GuideService.GetByName(name)
            );
        }

        [HttpGet]
        [Route("GetCount")]
        public IActionResult GetCount()
        {
            return Ok(
                _GuideService.GetCount()
            );
        }

        private string UploadFiles()
        {
            try
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
            
            } catch(Exception ex) {
                return string.Empty;
            }


        }


    }
}