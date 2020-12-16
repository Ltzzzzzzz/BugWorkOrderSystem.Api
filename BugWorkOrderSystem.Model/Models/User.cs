using System;
using SqlSugar;

namespace BugWorkOrderSystem.Model.Models
{
    /// <summary>
    /// 用户表
    /// </summary>
    public class User : RootEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 16)]
        public string Name { get; set; }
        /// <summary>
        /// 登陆账号
        /// </summary>
        [SugarColumn( Length = 10)]
        public string Account { get; set; }
        /// <summary>
        /// 登陆密码
        /// </summary>
        [SugarColumn(Length = 32)]
        public string Password { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        [SugarColumn(Length = 255, IsNullable = true)]
        public string Avatar { get; set; }
        /// <summary>
        /// 是否在线
        /// </summary>
        public bool Status { get; set; } = false;
        /// <summary>
        /// 尝试登陆错误次数
        /// </summary>
        public int ErrorCount { get; set; } = 0;
        /// <summary>
        /// 上次登陆时间
        /// </summary>
        public DateTime LastDate { get; set; } = DateTime.Now;
        /// <summary>
        /// 是否禁用
        /// </summary>
        public bool IsDisable { get; set; } = false;
        /// <summary>
        /// 再次尝试时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime AgainDate { get; set; }
    }
}
