using System;
using BugWorkOrderSystem.Common.Helper;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;

namespace BugWorkOrderSystem.Extensions.Extensions
{
    public static class SqlSugarSetup
    {
        public static void AddSqlSugarSetup(this IServiceCollection services)
        {
            services.AddScoped<ISqlSugarClient>(f =>
            {
                var conStr = Appsettings.app(new string[] { "ConnectionStr" });
                if (string.IsNullOrEmpty(conStr))
                {
                    throw new Exception("请确保appsettings里面有数据库连接字符串");
                }
                var sugar = new SqlSugarClient(new ConnectionConfig
                {
                    ConnectionString = conStr,
                    DbType = DbType.SqlServer,
                    InitKeyType = InitKeyType.Attribute,
                    IsAutoCloseConnection = true
                });

                return sugar;
            });
        }
    }
}
