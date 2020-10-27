using System;
using System.Threading.Tasks;
using BugWorkOrderSystem.Common.Helper;
using BugWorkOrderSystem.IServices;
using BugWorkOrderSystem.Model.ViewModels.Users;
using Microsoft.AspNetCore.Mvc;

namespace BugWorkOrderSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IUserRoleServices services;

        public LoginController(IUserRoleServices Services)
        {
            services = Services;
        }
        /// <summary>
        /// 登陆
        /// </summary>
        /// <remarks>返回用户信息和token</remarks>
        /// <param name="login">登陆账号密码</param>
        /// <returns>返回用户信息和token</returns>
        [HttpPost]
        public async Task<ActionResult<dynamic>> Login(UserLogin login)
        {
            return await services.Login(login);
        }
        /// <summary>
        /// 获取密码MD5字符串
        /// </summary>
        /// <remarks>返回密码MD5字符串</remarks>
        /// <param name="str">密码</param>
        /// <returns>返回密码MD5字符串</returns>
        [HttpGet("GetMD5Password")]
        public async Task<ActionResult<string>> GetMD5Password(string str)
        {
            return await Task<string>.Run(() => Md5Helper.Md5Str(str));
        }
    }
}
