using System;
using System.Collections.Generic;

namespace BugWorkOrderSystem.Model
{
    /// <summary>
    /// 分页类
    /// </summary>
    public class PageModel<T>
    {
        /// <summary>
        /// 页数
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount { get; set; }
        /// <summary>
        /// 行数
        /// </summary>
        public int Rows { get; set; }
        /// <summary>
        /// 数据总数
        /// </summary>
        public int DataCount { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public IEnumerable<T> Data { get; set; }
    }
}
