using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugWorkOrderSystem.Common.Db;
using BugWorkOrderSystem.Common.Helper;
using BugWorkOrderSystem.Model.DbSeed;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BugWorkOrderSystem.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // 此方法可以用于在启动时访问有作用域的服务以便运行初始化任务。
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var sugarContext = services.GetRequiredService<SugarContext>();
                    var initDb = services.GetRequiredService<InitDb>();
                    await initDb.InitAsync(sugarContext);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
