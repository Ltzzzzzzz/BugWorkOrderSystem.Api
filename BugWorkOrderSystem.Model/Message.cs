using System;
namespace BugWorkOrderSystem.Model
{
    public class MessageModel<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Res { get; set; }
    }
}
