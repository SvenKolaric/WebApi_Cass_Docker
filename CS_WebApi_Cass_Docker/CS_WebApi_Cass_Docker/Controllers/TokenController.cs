using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CS_WebApi_Cass_Docker.Controllers
{
    //[Produces("application/json")]
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private IConfiguration _config;

        public TokenController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public IActionResult CreateToken([FromBody]Entities.Login login)
        {
            BL.Login.BLLogin blLoginProvider = new BL.Login.BLLogin();
            BL.Token.BLToken blTokenProvider = new BL.Token.BLToken(_config);

            IActionResult response = Unauthorized();
            
            var user = blLoginProvider.CheckLogin(login);

            if (user != null)
            {
                var tokenString = blTokenProvider.BuildToken(user);
                //Request.Headers["Authorization"] = "Bearer " + tokenString;
                response = Ok(new { token = tokenString });
            }
            else
            {
                return BadRequest("Could not verify username and password");
            }
            return response;
        }
    }
}