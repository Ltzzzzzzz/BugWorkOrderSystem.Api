using System;
using System.IO;
using BugWorkOrderSystem.Common.Helper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace BugWorkOrderSystem.Common.Extensions
{
    public static class SwaggerSetup
    {
        public static void AddSwaggerSetup(this IServiceCollection services)
        {
            services.AddSwaggerGen(o =>
            {
                var project = Appsettins.app(new string[] { "BaseInfo", "Project" });
                var ver = Appsettins.app(new string[] { "BaseInfo", "Version" });

                o.SwaggerDoc($"{project} {ver}", new OpenApiInfo
                {
                    Version = ver,
                    Title = $"{project} 接口文档 -- Netcore3.1",
                    Description = $"{project}.Api {ver}",
                    Contact = new OpenApiContact
                    {
                        Name = "Ltzzzzzzz",
                        Email = "495236549@qq.com",
                        Url = new Uri($"https://github.com/Ltzzzzzzz/{project}.Api")
                    }
                });

                o.OperationFilter<AddResponseHeadersFilter>();
                o.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                o.OperationFilter<SecurityRequirementsOperationFilter>();
                o.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JwT授权(数据将在请求头中进行传输)直接在下框中输入 Bearer{ token}(注意两者之间是空格)",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey

                });

                // 配置xml注释
                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, $"{project}.Api.xml");
                var modelXmlPath = Path.Combine(basePath, $"{project}.Model.xml");
                o.IncludeXmlComments(xmlPath, true);
                o.IncludeXmlComments(modelXmlPath);
            });
        }
    }
}
