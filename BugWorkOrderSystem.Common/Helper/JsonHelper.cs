using System;
using Newtonsoft.Json;

namespace BugWorkOrderSystem.Common.Helper
{
    public static class JsonHelper
    {
        /// <summary>
        /// 转Json回HttpResponseMessage
        /// </summary>
        /// <param name="code"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static string toJson(object result)
        {
            return JsonConvert.SerializeObject(result);
        }
    }
}
