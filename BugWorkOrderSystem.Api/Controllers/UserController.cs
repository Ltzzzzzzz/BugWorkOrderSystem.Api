using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BugWorkOrderSystem.Extensions.Authorization.Helper;
using BugWorkOrderSystem.IServices;
using BugWorkOrderSystem.Model;
using BugWorkOrderSystem.Model.Models;
using BugWorkOrderSystem.Model.ViewModels.UserProfiles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BugWorkOrderSystem.Api.Controllers
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize("CustomPolicy")]
    public class UserController : ControllerBase
    {
        private readonly IUserServices services;

        public UserController(IUserServices services)
        {
            this.services = services;
        }
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<MessageModel<dynamic>>> Login(UserLoginDto login)
        {
            var rs = await services.Login(login);
            if (rs.Success)
            {
                var uvd = rs.Data;
                // 权限组
                var roles = uvd.Roles.Select(r => r.Code);
                var token = JwtHelper.IssueJwt(new JwtModel { Uid = rs.Data.Id.ToString(), Role = string.Join(',', roles) });
                return new MessageModel<dynamic>
                {
                    Success = rs.Success,
                    Message = rs.Message,
                    Data = new
                    {
                        UserInfo = uvd,
                        Token = token
                    }
                };
            }
            else
            {
                return new MessageModel<dynamic>
                {
                    Success = rs.Success,
                    Message = rs.Message,
                    Data = null
                };
            }
        }
        /// <summary>
        /// 登出
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Logout")]
        [AllowAnonymous]
        public async Task<ActionResult<MessageModel<dynamic>>> Logout(Guid id)
        {
            return await services.Logout(id);
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        public async Task<ActionResult<MessageModel<UserViewDto>>> Update([FromForm]UserUpdateDto update)
        {
            return await services.Update(update);
        }
        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <param name="page">页数</param>
        /// <param name="rows">条数</param>
        /// <returns></returns>
        [HttpGet("GetUsers")]
        public async Task<ActionResult<MessageModel<PageModel<UsersViewDto>>>> GetUsers([FromQuery]int page, int rows)
        {
            return await services.QueryList(page, rows);
        }
        [HttpGet("GetUser")]
        public async Task<ActionResult<MessageModel<SysUserViewDto>>> GetUserById(Guid id)
        {
            return await services.GetUser(id);
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="add"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public async Task<ActionResult<MessageModel<UserViewDto>>> AddUser(UserAddDto add)
        {
            return await services.Add(add);
        }
        /// <summary>
        /// 删除用户（假删除）
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        [HttpDelete("Delete")]
        public async Task<ActionResult<MessageModel<dynamic>>> DeleteUser(Guid id)
        {
            return await services.DeleteUser(id);
        }
        /// <summary>
        /// 用户停用
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        [HttpPut("Disable")]
        public async Task<ActionResult<MessageModel<dynamic>>> DisableUser(Guid id)
        {
            return await services.DisableUser(id);
        }
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        [HttpGet("ResetPassword")]
        public async Task<ActionResult<MessageModel<dynamic>>> ResetPassword(Guid id)
        {
            return await services.ResetPassword(id);
        }
    }
}
