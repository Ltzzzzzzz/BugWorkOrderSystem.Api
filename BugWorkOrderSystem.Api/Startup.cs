using System;
using System.IO;
using Autofac;
using BugWorkOrderSystem.Common.Helper;
using BugWorkOrderSystem.Common.HttpContextUser;
using BugWorkOrderSystem.Extensions.Extensions;
using BugWorkOrderSystem.Extensions.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace BugWorkOrderSystem.Api
{
    public class Startup
    {
        private readonly IWebHostEnvironment env;

        private string CorsUrl { get; set; }

        private string CorsName { get; set; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            this.env = env;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(s =>
            {
                // 时间格式化
                s.SerializerSettings.DateFormatString = "yyyy-MM-dd";
            });
            // appsetting操作类
            services.AddSingleton(new Appsettings(Configuration));
            // swagger
            services.AddSwaggerSetup();
            // authentication
            services.AddAuthenticationSetup();
            // authorization
            services.AddAuthorizationSetup();
            // sqlsugar
            services.AddSqlSugarSetup();
            // dbsetup
            services.AddDBSetup();
            // automapper
            services.AddAutoMapperSetup();
            // 配置跨域
            services.AddCors(s =>
            {
                s.AddPolicy(CorsName, p =>
                {
                    p.WithOrigins(CorsUrl.Split(','))
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            // http相关
            services.AddHttpContextSetup();
        }
        // 配置Autofac
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModuleRegister());
        }

        public void Configure(IApplicationBuilder app)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                CorsUrl = Configuration["Cors:Dev:Url"];
                CorsName = Configuration["Cors:Dev:Name"];
            }

            app.UseSwagger();

            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("swagger/BugWorkOrderSystem.Api/swagger.json", "BugWorkOrderSystem.Api");
                s.RoutePrefix = string.Empty;
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(env.WebRootPath),
                RequestPath = new PathString("/src")
            });

            app.UseRouting();

            app.UseCors(CorsName);

            app.UseExceptionMiddleware();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
