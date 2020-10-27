using System;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace BugWorkOrderSystem.Common.Helper
{
    /// <summary>
    /// appsettings.json操作类
    /// </summary>
    public class Appsettins
    {
        private static IConfiguration Configuration;

        public Appsettins(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// 返回appsettings里面的字符
        /// </summary>
        /// <param name="sections"></param>
        /// <returns></returns>
        public static string app(params string[] sections)
        {
            try
            {
                if (sections.Any())
                {
                    return Configuration[string.Join(':', sections)];
                }
            }
            catch (Exception) { }
            return string.Empty;
        }
    }
}
