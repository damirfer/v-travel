using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class DashboardController : Controller
    {
        private readonly IDashboardService _DashboardService;

        public DashboardController(IDashboardService DashboardService)
        {
            _DashboardService = DashboardService;
        }



        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(
                _DashboardService.GetData()
            );
        }
    }
}