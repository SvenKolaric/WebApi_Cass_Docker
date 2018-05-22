using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Wangkanai.Detection;

namespace CS_WebApi_Cass_Docker.Controllers
{
    //[Produces("application/json")]
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private IConfiguration _config;
        private readonly IUserAgent _useragent;
        private readonly IDevice _device;
        private readonly IBrowser _browser;


        public TokenController(IConfiguration config, IDeviceResolver deviceResolver, IBrowserResolver browserResolver)
        {
            _config = config;
            _useragent = deviceResolver.UserAgent;
            _device = deviceResolver.Device;
            _browser = browserResolver.Browser;

        }

        [AllowAnonymous]
        [HttpPost] //get
        [IgnoreAntiforgeryToken]
        public IActionResult CreateToken([FromBody]Entities.Login login) //maknes ovo
        {
            BL.Login.BLLogin blLoginProvider = new BL.Login.BLLogin();
            BL.Token.BLToken blTokenProvider = new BL.Token.BLToken(_config);

            IActionResult response = Unauthorized();
            
            var user = blLoginProvider.CheckLogin(login); //makneš ovo

            //var user1 = new DTO.User();   //odkomentiraš ovo
            //user1.Email = "user.user@gmail.com";
            //user1.Password = "12345678";
            //user1.Role = "user";

            if (user != null)
            {
                var deviceName = _device.Type.ToString() + " " + _browser.Type.ToString();
                var tokenString = blTokenProvider.BuildToken(user, deviceName);
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