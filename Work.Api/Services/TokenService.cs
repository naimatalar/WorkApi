﻿//using System;
//using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
//using System.Linq;
//using System.Security.Claims;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.Extensions.Options;
//using Microsoft.IdentityModel.Tokens;
//using Work.Api.Helpers;

//namespace Work.Api.Services
//{
//    public class TokenService
//    {
//        public static string GenerateToken(Users agent, IOptions<AppSettings> appSettings)
//        {
//            var _appSettings = appSettings;
//            var tokenHandler = new JwtSecurityTokenHandler();
//            var key = Encoding.ASCII.GetBytes(_appSettings.Value.Secret);
//            var tokenDescriptor = new SecurityTokenDescriptor
//            {
//                Subject = new ClaimsIdentity(new Claim[]
//                {
//                    new Claim("userId", agent.Id.ToString()),

//                    new Claim("mailAddress", agent.MailAdress),


//                }),
//                Expires = DateTime.UtcNow.AddDays(7),
//                SigningCredentials =
//                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),

//            };
//            var token = tokenHandler.CreateToken(tokenDescriptor);
//            string tokenstr = tokenHandler.WriteToken(token);


//            return tokenstr;
//        }
//    }
//}
