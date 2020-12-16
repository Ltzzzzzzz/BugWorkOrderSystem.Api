using System;
using System.Collections.Generic;
using System.Security.Claims;
using BugWorkOrderSystem.Extensions.Authorization.Policy;
using BugWorkOrderSystem.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace BugWorkOrderSystem.Extensions.Extensions
{
    public static class AuthorizationSetup
    {
        public static void AddAuthorizationSetup(this IServiceCollection services)
        {
            var requirement = new Requirement
            {
                ClaimType = ClaimTypes.Role, // 基于角色授权
                Expiration = TimeSpan.FromSeconds(60 * 60), // 接口过期时间
                Roles = new List<Role>()
            };

            services.AddAuthorization(c =>
            {
                c.AddPolicy("CustomPolicy", p =>
                {
                    p.Requirements.Add(requirement);
                });
            });

            // 注入授权处理器
            services.AddScoped<IAuthorizationHandler, RequirementHandler>();
            //services.AddSingleton(requirement);
        }
    }
}
