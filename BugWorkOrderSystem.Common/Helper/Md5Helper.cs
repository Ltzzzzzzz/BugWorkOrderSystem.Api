using System.Security.Cryptography;
using System.Text;

namespace BugWorkOrderSystem.Common.Helper
{
    public class Md5Helper
    {
        public static string Md5Str(string str)
        {
            using (MD5 md5 = MD5.Create())
            {
                // 把字符串转成byte数组
                byte[] bytes = Encoding.UTF8.GetBytes(str);
                // 把byte数组转换成字符串
                // 2.调用该对象的方法进行MD5计算
                byte[] md5Bytes = md5.ComputeHash(bytes);
                // 3.把结果以字符串的形式返回，取回十六进制的字符串
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < md5Bytes.Length; i++)
                {
                    // ToString("x2")意思是返回十六进制字符串，并且补零
                    sb.Append(md5Bytes[i].ToString("x2"));
                }
                return sb.ToString();
            };
        }
    }
}
