using System;
using System.ComponentModel.DataAnnotations;
using BugWorkOrderSystem.Model.Models;

namespace BugWorkOrderSystem.Model.ViewModels.Users
{
    public class UserAdd : RootEntity
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        [Required(ErrorMessage = "请输入用户名称")]
        [StringLength(10, ErrorMessage = "用户名称最大长度是10")]
        public string Name { get; set; }
        /// <summary>
        /// 登陆账号
        /// </summary>
        [Required(ErrorMessage = "请输入登陆账号")]
        [MaxLength(20, ErrorMessage = "登陆账号最大长度是20")]
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "请输入密码")]
        public string Password { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        [Required]
        public Role Role { get; set; }
    }
}
