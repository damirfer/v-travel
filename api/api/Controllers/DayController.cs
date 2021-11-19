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
    public class DayController : Controller
    {
        private readonly IDayService _DayService;
        private readonly IHostingEnvironment _environment;

        public DayController(IDayService DayService,IHostingEnvironment environment)
        {
            _DayService = DayService;
            _environment = environment;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(
                _DayService.GetAll()
            );
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(
                _DayService.Get(id)
            );
        }

        [HttpGet]
        [Route("GetListItem/{id}")]
        public IActionResult GetListItem(int id)
        {
            return Ok(
                _DayService.GetListItem(id)
            );
        }

        [HttpPost]
        public IActionResult Post(Day model)
        {
            return Ok(
                _DayService.Add(model)
            );
        }

        [HttpPut]
        public IActionResult Put(Day model)
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
                _DayService.Update(model)
            );
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(
                _DayService.Delete(id)
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