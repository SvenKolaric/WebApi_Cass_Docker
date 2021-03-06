﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace CS_WebApi_Cass_Docker.Controllers
{
    //[Produces("application/json")]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        [AllowAnonymous]
        public string GetLogin()
        {
            return "Welcome";
        }

        [AllowAnonymous]
        [HttpPost] //get
        [IgnoreAntiforgeryToken]
        public IActionResult Login([FromBody]Entities.Login login) //maknes ovo
        {
            BL.Login.BLLogin blLoginProvider = new BL.Login.BLLogin();
            BL.Token.BLToken blTokenProvider = new BL.Token.BLToken(_config);

            IActionResult response = Unauthorized();

            var user = blLoginProvider.CheckLogin(login); //makneš ovo

            if (user != null)
            {
                var deviceName = blTokenProvider.GetDeviceName(Request.Headers["User-Agent"]);
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