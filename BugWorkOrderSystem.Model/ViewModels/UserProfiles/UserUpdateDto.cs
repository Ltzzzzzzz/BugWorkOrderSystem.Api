using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BugWorkOrderSystem.Model.ViewModels.UserProfiles
{
    public class UserUpdateDto
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 新密码
        /// </summary>
        [StringLength(32, ErrorMessage = "密码长度不对")]
        public string NewPassword { get; set; }
        /// <summary>
        /// 旧密码
        /// </summary>
        [StringLength(32, ErrorMessage = "密码长度不对")]
        public string OldPassword { get; set; }
        /// <summary>
        /// 头像文件
        /// </summary>
        public IFormFile Avatar { get; set; }
    }
}
