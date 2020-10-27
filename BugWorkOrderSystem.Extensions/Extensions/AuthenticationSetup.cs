using System;
using System.Text;
using BugWorkOrderSystem.Common.Helper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace BugWorkOrderSystem.Common.Extensions
{
    public static class AuthenticationSetup
    {
        public static void AddAuthenticationSetup(this IServiceCollection services)
        {
            var iss = Appsettins.app(new string[] { "JwtSettings", "Issuer" });
            var aud = Appsettins.app(new string[] { "JwtSettings", "Audience" });
            var secret = Appsettins.app(new string[] { "JwtSettings", "Secret" });

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));

            var parms = new TokenValidationParameters
            {
                // 验证颁发人
                ValidateIssuer = true,
                ValidIssuer = iss,
                // 验证受众
                ValidateAudience = true,
                ValidAudience = aud,
                // 验证过期时间
                ValidateLifetime = true,
                RequireExpirationTime = true,
                ClockSkew = TimeSpan.Zero,
                // 密钥
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key
            };
            services
                .AddAuthentication("Bearer")
                .AddJwtBearer(o => o.TokenValidationParameters = parms);
        }
    }
}
