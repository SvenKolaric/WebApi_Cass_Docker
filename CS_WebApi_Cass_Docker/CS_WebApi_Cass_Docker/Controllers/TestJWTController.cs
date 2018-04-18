using System;
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
        [Authorize]
        public IEnumerable<Users> Get()
        {
            var currentUser = HttpContext.User;
            var resultUserList = new Users[]
            {
                new Users {Email = "papa@papa", Pass = "password", IsAdmin = false },
                new Users {Email = "admin@admin.hr", Pass = "admin", IsAdmin = true }
            };

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
