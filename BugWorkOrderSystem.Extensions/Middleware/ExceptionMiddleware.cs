using System;
using System.Threading.Tasks;
using BugWorkOrderSystem.Common.Helper;
using BugWorkOrderSystem.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using SqlSugar;

namespace BugWorkOrderSystem.Extensions.Middleware
{
    /// <summary>
    /// 注册http管道注入中间件
    /// </summary>
    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }

    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e.Message);
            }
            finally
            {
                var isExpired = context.Response.Headers["Token-Expired"].ObjToBool();
                var status = context.Response.StatusCode;
                var msg = string.Empty;
                switch (status)
                {
                    case 400:
                        msg = "请求参数错误";
                        break;
                    case 401:
                        msg = isExpired ? "登陆验证过期，请重新登陆" : "很抱歉，您无权访问该接口，请确保已经登录!";
                        break;
                    case 403:
                        msg = "很抱歉，您的访问权限等级不够，联系管理员!";
                        break;
                    case 404:
                        msg = "很抱歉，未找到服务";
                        break;
                    case 405:
                        msg = "405 Method Not Allowed";
                        break;
                    case 502:
                        msg = "请求错误";
                        break;
                }
                if (!string.IsNullOrEmpty(msg))
                {
                    await HandleExceptionAsync(context, msg);
                }
            }
        }
        private async Task HandleExceptionAsync(HttpContext httpContext, string msg)
        {
            var error = new MessageModel<dynamic>
            {
                Success = false,
                Message = msg,
                Data = null
            };
            var result = JsonHelper.toJson(error);
            httpContext.Response.ContentType = "application/json;charset=utf-8";
            await httpContext.Response.WriteAsync(result).ConfigureAwait(false);
        }
    }
}
