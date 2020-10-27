using System;
using SqlSugar;

namespace BugWorkOrderSystem.Model.Models
{
    /// <summary>
    /// 角色权限关联表
    /// </summary>
    public class RolePermission : RootEntity
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public Guid RoleId { get; set; }
        /// <summary>
        /// 权限Id
        /// </summary>
        public Guid PermissionId { get; set; }
    }
}
