using System.Threading.Tasks;
using BugWorkOrderSystem.Model;
using BugWorkOrderSystem.Model.ViewModels.Users;

namespace BugWorkOrderSystem.IServices
{
    public interface IUserRoleServices
    {
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="login">登陆信息</param>
        /// <returns>返回Jwt</returns>
        Task<MessageModel<dynamic>> Login(UserLogin login);

    }
}
