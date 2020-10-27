using System;
using SqlSugar;

namespace BugWorkOrderSystem.Model.Models
{
    /// <summary>
    /// 菜单表
    /// </summary>
    public class Menu : RootEntity
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 10)]
        public string Name { get; set; }
        /// <summary>
        /// icon
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 父Id
        /// </summary>
        public Guid? Pid { get; set; }
        /// <summary>
        /// 模块
        /// </summary>
        public string Model { get; set; }
    }
}
