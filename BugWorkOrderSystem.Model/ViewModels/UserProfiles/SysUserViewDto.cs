using System;
using System.Collections.Generic;
using BugWorkOrderSystem.Model.ViewModels.RoleProfiles;

namespace BugWorkOrderSystem.Model.ViewModels.UserProfiles
{
    public class SysUserViewDto
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 用户头像
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator { get; set; }
        /// <summary>
        /// 上次登陆日期
        /// </summary>
        public DateTime LastDate { get; set; }
        /// <summary>
        /// 用户权限组
        /// </summary>
        public List<RoleViewDto> Roles { get; set; }
        /// <summary>
        /// 是否初始用户
        /// </summary>
        public bool IsInit { get; set; }
    }
}
