using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace CS_WebApi_Cass_Docker.Controllers
{
    [Produces("application/json")]
    [Route("api/Blacklisted")]
    [Authorize]
    public class BlacklistedController : Controller
    {
        // GET: api/Blacklisted
        [HttpGet]
        public IEnumerable<Entities.Token> GetUserBlacklist()
        {
            BL.Token.BLToken blProvider = new BL.Token.BLToken();

            var emailClaim = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Email).FirstOrDefault();

            return blProvider.GetListOfUserTokens(emailClaim.Value);
        }

        // GET: api/Blacklisted/5
        [HttpGet("{id}", Name = "GetBlacklistID")]
        public string GetBlacklistID(int id)
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
        [HttpDelete("{email, devicename}")]
        public void Delete(string email, string deviceName)
        {
            BL.Token.BLToken blProvider = new BL.Token.BLToken();
            blProvider.DeleteToken(email, deviceName);
        }
    }
}
