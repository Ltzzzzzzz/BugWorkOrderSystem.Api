using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BugWorkOrderSystem.Model.ViewModels.UserProfiles
{
    public class UserAddDto
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        [Required(ErrorMessage = "请输入用户名称")]
        [MaxLength(16, ErrorMessage = "用户名称最大长度是16")]
        [MinLength(3, ErrorMessage = "用户名称最小长度是3")]
        public string Name { get; set; }
        /// <summary>
        /// 登陆账号
        /// </summary>
        [Required(ErrorMessage = "请输入登陆账号")]
        [MaxLength(16, ErrorMessage = "登陆账号最大长度是16")]
        [MinLength(3, ErrorMessage = "登陆账号最小长度是3")]
        public string Account { get; set; }
        /// <summary>
        /// 登陆密码
        /// </summary>
        [Required(ErrorMessage = "请输入登陆密码")]
        [StringLength(32, ErrorMessage = "密码长度不对")]
        public string Password { get; set; }
        /// <summary>
        /// 用户权限
        /// </summary>
        [Required(ErrorMessage = "请选择权限")]
        public List<Guid> Roles { get; set; }
    }
}
