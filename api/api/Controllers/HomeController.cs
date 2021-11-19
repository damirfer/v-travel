using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model.ViewModels;
using Service;

namespace api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private IHomeService _HomeService;
        private IConfiguration _configuration;
        public HomeController(IHomeService homeService, IConfiguration configuration)
        {
            _configuration = configuration;
            _HomeService = homeService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] LoginVM request)
        {
            int travelerId=0;
            if (_HomeService.CheckCredentials(request,ref travelerId))
            {

                var claims = new[]
                {
                    new Claim(ClaimTypes.Sid, travelerId.ToString())
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecurityKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: "dev",
                    audience: "dev",
                    claims: claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: creds);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }

            return BadRequest("Could not verify username and password");
        }




    }
}