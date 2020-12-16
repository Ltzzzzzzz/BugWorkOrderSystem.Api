using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using BugWorkOrderSystem.Common.Helper;
using Microsoft.IdentityModel.Tokens;

namespace BugWorkOrderSystem.Extensions.Authorization.Helper
{
    public static class JwtHelper
    {
        /// <summary>
        /// 颁发Jwt
        /// </summary>
        /// <param name="model"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static string IssueJwt(JwtModel model)
        {
            var iss = Appsettings.app(new string[] { "JwtSettings", "Issuer" });
            var aud = Appsettings.app(new string[] { "JwtSettings", "Audience" });
            var secret = Appsettings.app(new string[] { "JwtSettings", "Secret" });

            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, model.Uid), // Uid
                new Claim(JwtRegisteredClaimNames.Iss, iss), // Issuer
                new Claim(JwtRegisteredClaimNames.Aud, aud), // Audience
                new Claim(JwtRegisteredClaimNames.Iat, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"), // 何时生效
                new Claim(JwtRegisteredClaimNames.Nbf, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"), // 何时之前不生效
                new Claim(JwtRegisteredClaimNames.Exp , $"{new DateTimeOffset(DateTime.Now).AddHours(1).ToUnixTimeSeconds()}"), // 过期时间
                new Claim(ClaimTypes.Expiration, DateTime.Now.AddHours(1).ToString()),
            };
            // 多角色
            claims.AddRange(model.Role.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(r => new Claim(ClaimTypes.Role, r)));

            var jwt = new JwtSecurityToken(issuer: iss, claims: claims, signingCredentials: creds);
            var handler = new JwtSecurityTokenHandler();

            return handler.WriteToken(jwt);
        }
        /// <summary>
        /// 解析Jwt
        /// </summary>
        /// <param name="jwt"></param>
        public static JwtModel SerializeJwt(string jwtStr)
        {
            var token = jwtStr.Split("Bearer ")[1];
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            object role, uid;
            try
            {
                jwt.Payload.TryGetValue(ClaimTypes.Role, out role);
                jwt.Payload.TryGetValue(JwtRegisteredClaimNames.Jti, out uid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            var jm = new JwtModel { Uid = uid.ToString(), Role = role.ToString() };
            return jm;
        }
    }
    /// <summary>
    /// 令牌类
    /// </summary>
    public class JwtModel
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string Uid { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Role { get; set; }
    }
}
