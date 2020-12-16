using System;
using System.Collections.Generic;
using BugWorkOrderSystem.Model.ViewModels.RoleProfiles;

namespace BugWorkOrderSystem.Model.ViewModels.UserProfiles
{
    public class UserViewDto
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
        /// 是否初始用户
        /// </summary>
        public bool IsInit { get; set; }
        /// <summary>
        /// 用户权限组
        /// </summary>
        public List<RoleViewDto> Roles { get; set; }
    }
}
