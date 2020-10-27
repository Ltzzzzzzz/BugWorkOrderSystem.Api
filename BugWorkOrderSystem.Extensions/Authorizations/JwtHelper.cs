using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BugWorkOrderSystem.Model.Models;
using Microsoft.IdentityModel.Tokens;

namespace BugWorkOrderSystem.Common.Helper
{
    public static class JwtHelper
    {
        /// <summary>
        /// 颁发Jwt
        /// </summary>
        /// <returns></returns>
        public static string IssueJwt(string role)
        {
            var iss = Appsettins.app(new string[] { "JwtSettings", "Issuer" });
            var aud = Appsettins.app(new string[] { "JwtSettings", "Audience" });
            var secret = Appsettins.app(new string[] { "JwtSettings", "Secret" });

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iss, iss),
                new Claim(JwtRegisteredClaimNames.Aud, aud),
                new Claim(JwtRegisteredClaimNames.Iat, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Nbf, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Exp, $"{new DateTimeOffset(DateTime.Now.AddSeconds(1000)).ToUnixTimeSeconds()}"),
            };

            var jwt = new JwtSecurityToken(issuer: iss, claims: claims, signingCredentials: creds);
            var jwtHandler = new JwtSecurityTokenHandler();

            return jwtHandler.WriteToken(jwt);
        }
    }
}
