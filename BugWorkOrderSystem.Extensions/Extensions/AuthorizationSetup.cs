using System;
using BugWorkOrderSystem.Common.Policys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace BugWorkOrderSystem.Common.Extensions
{
    public static class Authorization
    {
        public static void AddAuthorizationSetup(this IServiceCollection services)
        {
            services.AddAuthorization(o =>
            {
                o.AddPolicy("CustomPolicy", policy =>
                {
                    policy.Requirements.Add(new Permission());
                });
            });

            // 注入权限处理器
            services.AddScoped<IAuthorizationHandler, PermissionHandler>();
        }
    }
}
