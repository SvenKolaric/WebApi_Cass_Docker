﻿using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace CS_WebApi_Cass_Docker.Controllers
{
    //just testing
    public class Users
    {
        public string Email { get; set; }
        public string Pass { get; set; }
        public bool IsAdmin { get; set; }
    }

    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TestJWTController : Controller
    {
        // GET: api/TestJWT
        [HttpGet]
        //[Authorize]
        [Authorize(Policy = "User")]
        public IEnumerable<Users> Get()
        {
            var currentUser = HttpContext.User; //tututu!!!!!!!!!!!!! info o korisniku
                                                //var name = User.Identity.Name;


            //// Inside on of your controllers
            //if (User.Identity.IsAuthenticated)
            //{
            //    string accessToken = await HttpContext.GetTokenAsync("access_token");
            //    string idToken = await HttpContext.GetTokenAsync("id_token");

            //    // Now you can use them. For more info on when and how to use the 
            //    // access_token and id_token, see https://auth0.com/docs/tokens
            //}

            string authorization = Request.Headers["Authorization"];
            var currentUser2 = HttpContext.User.Identity.Name;
            // If no authorization header found, nothing to process further


            if (authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                var token = authorization.Substring("Bearer ".Length).Trim();
                // If no token found, no further work possible
            }
            
            var expiration = currentUser.Claims.ElementAt(3).Value;

            var resultUserList = new Users[]
            {
                new Users {Email = "Bravo", Pass = "Ušel si", IsAdmin = false },
            };

            //if (currentUser.HasClaim(c => c.Type == ClaimTypes.DateOfBirth))
            //{
            //    DateTime birthDate = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.DateOfBirth).Value);
            //    userAge = DateTime.Today.Year - birthDate.Year;
            //}

            //if (userAge < 18)
            //{
            //    resultBookList = resultBookList.Where(b => !b.AgeRestriction).ToArray();
            //}

            return resultUserList;
        }

        // GET: api/TestJWT/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/TestJWT
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/TestJWT/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
