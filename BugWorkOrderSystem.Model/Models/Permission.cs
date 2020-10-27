using System;
using SqlSugar;

namespace BugWorkOrderSystem.Model.Models
{
    /// <summary>
    /// 权限表
    /// </summary>
    public class Permission : RootEntity
    {
        /// <summary>
        /// 权限名称
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 10)]
        public string Name { get; set; }
    }
}
