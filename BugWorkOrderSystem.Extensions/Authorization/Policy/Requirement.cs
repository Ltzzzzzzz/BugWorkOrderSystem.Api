using System;
using System.Collections.Generic;
using BugWorkOrderSystem.Model.Models;
using Microsoft.AspNetCore.Authorization;

namespace BugWorkOrderSystem.Extensions.Authorization.Policy
{
    public class Requirement : IAuthorizationRequirement
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public List<Role> Roles { get; set; }
        /// <summary>
        /// 认证授权类型
        /// </summary>
        public string ClaimType { internal get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public TimeSpan Expiration { get; set; }
    }
}
