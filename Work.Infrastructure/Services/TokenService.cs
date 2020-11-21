using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Work.Core;
using Work.Infrastructure.Helpers;
using Work.Infrastructure.ServicesInterface;

namespace Work.Infrastructure.Services
{
   public class TokenService:ITokenService
    {
        public string GenerateToken(User user, IOptions<AppSettings> appSettings)
        {
            var _appSettings = appSettings;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Value.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("userId", user.Id.ToString()),

                    new Claim("mailAddress", user.Email),


                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenstr = tokenHandler.WriteToken(token);


            return tokenstr;
        }
    }
}
