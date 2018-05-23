using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CS_WebApi_Cass_Docker.Controllers
{
    [Produces("application/json")]
    [Route("api/Blacklisted")]
    public class BlacklistedController : Controller
    {
        // GET: api/Blacklisted
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Blacklisted/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Blacklisted
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Blacklisted/5
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
