using System;
using System.Collections.Generic;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BL.Token
{
    public class TokenValidation
    {
        public bool CheckTokenExpDate(JwtSecurityToken _token)
        {
            return false;
        }
    }
}
