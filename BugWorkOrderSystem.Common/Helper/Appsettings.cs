using System;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace BugWorkOrderSystem.Common.Helper
{
    /// <summary>
    /// appsetting.json操作类
    /// </summary>
    public class Appsettings
    {
        private static IConfiguration configuration;

        public Appsettings(IConfiguration Configuration)
        {
            configuration = Configuration;
        }
        /// <summary>
        /// 获取appsettings.json字符
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        public static string app(params string[] section)
        {
            try
            {
                if (section.Any())
                {
                    return configuration[string.Join(':', section)];
                }
            }
            catch { }
            return string.Empty;
        }
    }
}
