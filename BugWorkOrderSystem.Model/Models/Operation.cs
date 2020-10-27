using SqlSugar;
namespace BugWorkOrderSystem.Model.Models
{
    /// <summary>
    /// 权限操作表
    /// </summary>
    public class Operation : RootEntity
    {
        /// <summary>
        /// 操作名称
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 10)]
        public string Name { get; set; }
        /// <summary>
        /// 操作编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 操作方法
        /// </summary>
        public string Method { get; set; }
        /// <summary>
        /// 操作链接
        /// </summary>
        public string Url { get; set; }
    }
}
