using System;
using SqlSugar;

namespace BugWorkOrderSystem.Model.Models
{
    /// <summary>
    /// 菜单权限关联表
    /// </summary>
    public class PermissionMeun : RootEntity
    {
        /// <summary>
        /// 权限Id
        /// </summary>
        public Guid PermissinoId { get; set; }
        /// <summary>
        /// 菜单Id
        /// </summary>
        public Guid MenuId { get; set; }
    }
}
