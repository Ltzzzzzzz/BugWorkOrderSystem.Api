using BugWorkOrderSystem.Common.Db;
using Microsoft.Extensions.DependencyInjection;

namespace BugWorkOrderSystem.Common.Extensions
{
    public static class SqlSugarSetup
    {
        public static void AddSqlSugarSetup(this IServiceCollection services)
        {
            // 注入Sugar
            services.AddScoped<SugarContext>();
        }
    }
}
