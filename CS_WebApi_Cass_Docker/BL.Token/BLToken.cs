using System;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;


namespace BL.Token
{
    public class BLToken
    {
        private IConfiguration _config;

        public BLToken(IConfiguration config)
        {
            _config = config;
        }        

        public string BuildToken(DTO.User user)
        {
            var claims = BuildClaims(user);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                notBefore: DateTime.Now,//.AddHours(2), //naše vrijeme
                expires: DateTime.Now.AddMinutes(1),//AddHours(2).AddMinutes(30), //naše vrijeme
                signingCredentials: creds);

            SaveTokenToDB(user.Email, token);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private Claim[] BuildClaims(DTO.User user)
        {
            //Define claims in the token
            var claims = new[]
            {
                new Claim("Role", user.Role),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            return claims;
        }

        private void SaveTokenToDB(string _email, JwtSecurityToken _token)
        {
            DAL.TokenDB dalProvider = new DAL.TokenDB();

            Entities.Token t = new Entities.Token
            {
                TokenData = new JwtSecurityTokenHandler().WriteToken(_token),
                Email = _email,
                CreationDate = _token.ValidFrom,
                ExpirationDate = _token.ValidTo
            };

            dalProvider.SaveToken(t);
        }
    }
}
