using System;
using System.Security.Cryptography;
using System.Text;

namespace BugWorkOrderSystem.Common.Helper
{
    public static class MD5Helper
    {
        public static string Md5Str(string str)
        {
            using (var md5 = MD5.Create())
            {
                // 将字符串转为字节组
                var bytes = Encoding.UTF8.GetBytes(str);
                // 计算md5字节组
                var md5Bytes = md5.ComputeHash(bytes);

                var sb = new StringBuilder();
                for (int i = 0; i < md5Bytes.Length; i++)
                {
                    sb.Append(md5Bytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
}
