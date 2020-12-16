using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace BugWorkOrderSystem.Extensions.Extensions
{
    public static class SwaggerSetup
    {
        public static void AddSwaggerSetup(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("BugWorkOrderSystem.Api", new OpenApiInfo
                {
                    Version = "V1",
                    Title = "BugWorkOrderSystem.Api",
                    Description = "BugWorkOrderSystem --接口文档 Netcore3.1",
                    Contact = new OpenApiContact
                    {
                        Name = "Ltzzzzzzz",
                        Email = "495236549@qq.com",
                        Url = new Uri("https://github.com/Ltzzzzzzz")
                    }
                });
                // xml注释
                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "BugWorkOrderSystem.Api.xml");
                s.IncludeXmlComments(xmlPath, true);
                var modelXmlPath = Path.Combine(basePath, "BugWorkOrderSystem.Model.xml");
                s.IncludeXmlComments(modelXmlPath);

                #region 加锁
                s.OperationFilter<AddResponseHeadersFilter>();
                s.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                var openApiSecurity = new OpenApiSecurityScheme
                {
                    Description = "JWT认证授权，使用直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",  //jwt 默认参数名称
                    In = ParameterLocation.Header,  //jwt默认存放Authorization信息的位置（请求头）
                    Type = SecuritySchemeType.ApiKey
                };

                s.AddSecurityDefinition("oauth2", openApiSecurity);
                // 在header中添加token，传递到后台
                s.OperationFilter<SecurityRequirementsOperationFilter>();
                #endregion
            });
        }
    }
}
