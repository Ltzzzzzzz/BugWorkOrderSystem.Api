using System;
using SqlSugar;
namespace BugWorkOrderSystem.Model.Models
{
    /// <summary>
    /// 操作权限关联表
    /// </summary>
    public class PermissionOperation : RootEntity
    {
        /// <summary>
        /// 权限Id
        /// </summary>
        public Guid PermissinoId { get; set; }
        /// <summary>
        /// 操作Id
        /// </summary>
        public Guid OperationsId { get; set; }
    }
}
