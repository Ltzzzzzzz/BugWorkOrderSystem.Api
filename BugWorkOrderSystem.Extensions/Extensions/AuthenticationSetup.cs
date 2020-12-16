using System;
using System.Text;
using System.Threading.Tasks;
using BugWorkOrderSystem.Common.Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace BugWorkOrderSystem.Extensions.Extensions
{
    public static class AuthenticationSetup
    {
        public static void AddAuthenticationSetup(this IServiceCollection services)
        {
            var iss = Appsettings.app(new string[] { "JwtSettings", "Issuer" });
            var aud = Appsettings.app(new string[] { "JwtSettings", "Audience" });
            var secret = Appsettings.app(new string[] { "JwtSettings", "Secret" });

            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));

            var tokenParams = new TokenValidationParameters
            {
                // 验证密钥
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = securityKey,
                // 验证iss
                ValidateIssuer = true,
                ValidIssuer = iss,
                // 验证aud
                ValidateAudience = true,
                ValidAudience = aud,
                // 验证过期时间
                ValidateLifetime = true,
                RequireExpirationTime = true,
                // 过期缓存时间
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication("Bearer").AddJwtBearer(o =>
            {
                o.TokenValidationParameters = tokenParams;

                o.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = c =>
                    {
                        // token过期在响应头添加Token-Expired
                        if (c.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            c.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });
        }
    }
}
