using System;
namespace BugWorkOrderSystem.Model
{
    /// <summary>
    /// 响应类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MessageModel<T>
    {
        /// <summary>
        /// 请求是否成功
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public T Data { get; set; }
    }
}
