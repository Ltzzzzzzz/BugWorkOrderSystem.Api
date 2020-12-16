using System;
using SqlSugar;

namespace BugWorkOrderSystem.Model.Models
{
    /// <summary>
    /// 角色菜单关联表
    /// </summary>
    public class RoleMenu
    {
        /// <summary>
        /// 自身Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// 角色Id
        /// </summary>
        public Guid RoleId { get; set; }
        /// <summary>
        /// 菜单Id
        /// </summary>
        public Guid MenuId { get; set; }
    }
}
