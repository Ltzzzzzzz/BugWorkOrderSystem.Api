using System;
using SqlSugar;

namespace BugWorkOrderSystem.Model.Models
{
    /// <summary>
    /// 用户角色关联表
    /// </summary>
    public class UserRole
    {
        /// <summary>
        /// 自身Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// 角色Id
        /// </summary>
        public Guid RoleId { get; set; }
    }
}
