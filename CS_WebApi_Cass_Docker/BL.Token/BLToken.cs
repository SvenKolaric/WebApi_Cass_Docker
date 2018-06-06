using System;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using UAParser;
using System.Linq;



namespace BL.Token
{
    public class BLToken
    {
        private IConfiguration _config;
        private DAL.TokenDB _dalProvider = new DAL.TokenDB();

        public BLToken()
        {
        }        

        public BLToken(IConfiguration config)
        {
            this._config = config;
        }

        public string BuildToken(DTO.User user, string deviceName)
        {
            TokenValidation tokenValidation = new TokenValidation();

            DateTime issuedAt = DateTime.Now;
            DateTime expires = DateTime.Now.AddHours(24);

            var claims = BuildClaims(user, issuedAt.ToString(), expires.ToString());
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY")));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: expires,
                signingCredentials: creds);

            //var existingToken = tokenValidation.CheckIfTokenExists(token.ToString(), user.Email, deviceName);
            //if (existingToken != null)
            //{
            //    return existingToken.TokenData;
            //}

            SaveTokenToDB(user.Email, token, issuedAt, expires,deviceName);
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private Claim[] BuildClaims(DTO.User user, string iat, string exp)
        {
            //Define claims in the token
            var claims = new[]
            {
                new Claim("Role", user.Role),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Iat, iat),
                new Claim(JwtRegisteredClaimNames.Exp, exp)
                //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            return claims;
        }

        private void SaveTokenToDB(string _email, JwtSecurityToken _token, DateTime _iat, DateTime _exp, string _deviceName)
        {
            Entities.Token t = new Entities.Token
            {
                TokenData = new JwtSecurityTokenHandler().WriteToken(_token),
                Email = _email,
                CreationDate = _iat,
                ExpirationDate = _exp,
                DeviceName = _deviceName
            };

            _dalProvider.SaveToken(t);
            //var tt1 = dalProvider.GetToken("user.user@gmail.com"); //get token
        }

        public string GetDeviceName(string _userAgent)
        {
            var uaParser = Parser.GetDefault();

            ClientInfo c = uaParser.Parse(_userAgent);

            if (c.OS.Family.Equals("Other") && c.UserAgent.Family.Equals("Other") && c.Device.Family.Equals("Other")) return c.String;
            else if (c.OS.Family.Equals("Other") && c.UserAgent.Family.Equals("Other")) return c.Device.Family;
            else return $"{c.OS.Family} {c.OS.Major}, {c.UserAgent.Family} - {c.Device.Family}";
        }

        //blacklist

        public IEnumerable<Entities.Token> GetListOfUserTokens(string _email)
        {
            var result = _dalProvider.GetTokensByUser(_email);

            return result;
        }

        public void DeleteToken(string _email, string _deviceName)
        {
            _dalProvider.DeleteToken(_email, _deviceName);
        }
    }
}
