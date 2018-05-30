using System;
using System.Collections.Generic;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BL.Token
{
    public class TokenValidation
    {
        public TokenValidation()
        {

        }

        public Entities.Token CheckIfTokenExistsAndValid(string _token, string _email, string _deviceName )
        {
            DAL.TokenDB dalProvider = new DAL.TokenDB();

            var token = dalProvider.GetToken(_email, _deviceName);

            if(token != null)
            {
                if (token.ExpirationDate >= DateTime.Now)
                {
                    return token;
                }
            }

            return null;
        }

        public bool CheckIfTokenBlacklisted(string _token, string _email, string _deviceName)
        {
            DAL.TokenDB dalProvider = new DAL.TokenDB();

            var token = dalProvider.GetToken(_email, _deviceName);

            if (token != null)
            {
                if(token.Blacklisted == true)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
