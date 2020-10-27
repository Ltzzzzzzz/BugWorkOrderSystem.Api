using System;
using BugWorkOrderSystem.Common.Extensions;
using BugWorkOrderSystem.Common.Helper;
using BugWorkOrderSystem.IServices;
using BugWorkOrderSystem.Model.DbSeed;
using BugWorkOrderSystem.Repository.Base;
using BugWorkOrderSystem.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BugWorkOrderSystem.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        /// <summary>
        /// 环境
        /// </summary>
        private readonly IWebHostEnvironment Env;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            // 注入Appsettings操作类
            services.AddSingleton(new Appsettins(Configuration));
            // 注入Md5Helper
            services.AddSingleton<Md5Helper>();
            // 注入Swagger
            services.AddSwaggerSetup();
            // 注入验证
            services.AddAuthenticationSetup();
            // 注入授权
            services.AddAuthorizationSetup();
            // 注入SqlSugar
            services.AddSqlSugarSetup();
            // 注入InitDb
            services.AddSingleton<InitDb>();
            // 注入泛型仓储
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            // 注入用户角色服务
            services.AddScoped<IUserRoleServices, UserRoleServices>();
            // 配置跨域
            services.AddCors(o =>
            {
                if (Env.IsDevelopment())
                {
                    var origins = Configuration["CorsLink:Dev"].Split(',', StringSplitOptions.RemoveEmptyEntries);
                    o.AddPolicy(Env.EnvironmentName, b =>
                    {
                        b.WithOrigins(origins)
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
                }
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // 使用swagger
            app.UseSwagger();
            app.UseSwaggerUI(o =>
            {
                var project = Appsettins.app(new string[] { "BaseInfo", "Project" });
                var ver = Appsettins.app(new string[] { "BaseInfo", "Version" });
                o.SwaggerEndpoint($"/swagger/{project} {ver}/swagger.json", $"{project} {ver}");
                o.RoutePrefix = string.Empty;
            });
            app.UseRouting();
            app.UseCors(env.EnvironmentName);
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
