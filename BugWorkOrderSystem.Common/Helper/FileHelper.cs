using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace BugWorkOrderSystem.Common.Helper
{
    public class FileHelper : IDisposable
    {
        private bool _alreadyDispose = false;

        public FileHelper()
        {
        }
        ~FileHelper()
        {
            Dispose(false);
        }
        protected virtual void Dispose(bool isDisposing)
        {
            if (_alreadyDispose) return;
            _alreadyDispose = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #region 取得文件后缀名
        /// <summary>
        /// 取后缀名
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>.gif|.html格式</returns>
        public static string GetPostfixStr(string filename)
        {
            return Path.GetExtension(filename);
        }
        #endregion

        #region 检测指定路径是否存在
        /// <summary>
        /// 检测指定路径是否存在
        /// </summary>
        /// <param name="path">目录的绝对路径</param> 
        public static bool IsExistDirectory(string path)
        {
            return Directory.Exists(path);
        }
        #endregion

        #region 创建文件夹
        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="folderPath">文件夹的绝对路径</param>
        public static void CreateFolder(string folderPath)
        {
            if (!IsExistDirectory(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }
        #endregion

        #region 文件转字节
        /// <summary>
        /// 文件转字节
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns></returns>
        public static byte[] FileToBytes(IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            {
                var bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                return bytes;
            }
        }
        #endregion

        #region 写入文件
        /// <summary>
        /// 写入路径
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="bytes">字节流</param>
        public static async System.Threading.Tasks.Task WriteAsync(string path, byte[] bytes)
        {
            using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                await fs.WriteAsync(bytes);
            }
        }
        #endregion
    }
}
