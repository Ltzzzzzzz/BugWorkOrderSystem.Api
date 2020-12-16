using System;
using SqlSugar;

namespace BugWorkOrderSystem.Model
{
    public class RootEntity
    {
        /// <summary>
        /// 实体主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public Guid Id { get; set; }
        /// <summary>
        /// 是否已删除
        /// </summary>
        public bool IsDeleted { get; set; } = false;
        /// <summary>
        /// 是否初始
        /// </summary>
        public bool IsInit { get; set; } = false;
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator { get; set; } = "系统";
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
