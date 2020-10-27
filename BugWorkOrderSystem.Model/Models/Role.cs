using System;
using SqlSugar;

namespace BugWorkOrderSystem.Model.Models
{
    /// <summary>
    /// 角色表
    /// </summary>
    public class Role : RootEntity
    {
        /// <summary>
        /// 角色名
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 10)]
        public string Name { get; set; }
    }
}
