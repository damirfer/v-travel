using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model.ViewModels;
using Service;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private IAuthService service;
        private IConfiguration configuration;
        public AuthController(IAuthService authService, IConfiguration conf)
        {
            service = authService;
            configuration = conf;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] LoginVM request)
        {
            int userId = 0;
            if (service.CheckCredentials(request, ref userId))
            {

                var claims = new[]
                {
                    new Claim(ClaimTypes.Sid, userId.ToString())
                };
                var security = configuration["SecurityKey"];
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SecurityKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: "dev",
                    audience: "dev",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }

            return BadRequest("Could not verify username and password");
        }

        [HttpGet]
        [Route("GetData")]
        public IActionResult GetData()
        {

            try
            {
                if(User.FindFirst(ClaimTypes.Sid) != null)
                {

                    var userId = Int32.Parse(User.FindFirst(ClaimTypes.Sid).Value);
                    return Ok(service.GetData(userId));
                }

                return Ok();
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }

    }

}
