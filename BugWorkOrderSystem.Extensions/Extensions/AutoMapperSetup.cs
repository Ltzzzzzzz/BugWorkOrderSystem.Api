using System;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace BugWorkOrderSystem.Extensions.Extensions
{
    public static class AutoMapperSetup
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
