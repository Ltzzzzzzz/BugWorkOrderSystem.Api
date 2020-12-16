using BugWorkOrderSystem.Model.DBSeed;
using Microsoft.Extensions.DependencyInjection;

namespace BugWorkOrderSystem.Extensions.Extensions
{
    public static class DBSetup
    {
        public static void AddDBSetup(this IServiceCollection services)
        {
            services.AddScoped<SugarContext>();
            services.AddScoped<Seed>();
        }
    }
}
