using System;
using System.Collections.Generic;
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
        /// 菜单图标
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string Icon { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        [SugarColumn(Length = 50)]
        public string Module { get; set; }
        /// <summary>
        /// 菜单序号
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 父级菜单Id
        /// </summary>
        public Guid Pid { get; set; }
        /// <summary>
        /// 是否需要权限
        /// </summary>
        public bool IsAuth { get; set; } = false;
        /// <summary>
        /// 子菜单
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<Menu> Children { get; set; }
    }
}
