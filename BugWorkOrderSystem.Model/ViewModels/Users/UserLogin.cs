using System.ComponentModel.DataAnnotations;

namespace BugWorkOrderSystem.Model.ViewModels.Users
{
    public class UserLogin
    {
        /// <summary>
        /// 登陆账号
        /// </summary>
        [Required(ErrorMessage = "请输入登陆账号")]
        [MaxLength(20, ErrorMessage = "登陆账号最大长度是20")]
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "请输入密码")]
        public string Password { get; set; }
    }
}
