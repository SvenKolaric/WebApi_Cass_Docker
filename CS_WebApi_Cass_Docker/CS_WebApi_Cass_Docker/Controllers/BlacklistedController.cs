using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;

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
        
        // DELETE: api/Blacklisted/Delete/user.user@gmail.com/Chrome%2066%20Windows%2010%20-%20Other (JavaScript function encodes space as %20)
        [HttpDelete("Delete/{email}/{deviceName}")]
        [IgnoreAntiforgeryToken]
        public HttpResponseMessage Delete(string email, string deviceName)
        {
            BL.Token.BLToken blProvider = new BL.Token.BLToken();
            blProvider.DeleteToken(email, deviceName);
            var response = new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK
            };
            return response;
        }
    }
}
