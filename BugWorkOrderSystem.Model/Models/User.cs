using System;
using SqlSugar;
namespace BugWorkOrderSystem.Model.Models
{
    /// <summary>
    /// 用户信息表
    /// </summary>
    public class User : RootEntity
    {
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; } = false;
        /// <summary>
        /// 名称
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 10)]
        public string Name { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string Avatar { get; set; }
        /// <summary>
        /// 登陆账号
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 20)]
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [SugarColumn(Length = 32)]
        public string Password { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public bool Status { get; set; } = false;
        /// <summary>
        /// 最后登陆时间
        /// </summary>
        public DateTime LastDate { get; set; } = DateTime.Now;
        /// <summary>
        /// 错误次数
        /// </summary>
        public int ErrorCount { get; set; } = 0;
    }
}
