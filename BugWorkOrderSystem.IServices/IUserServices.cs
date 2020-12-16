using System;
using System.Threading.Tasks;
using BugWorkOrderSystem.IServices.Base;
using BugWorkOrderSystem.Model;
using BugWorkOrderSystem.Model.Models;
using BugWorkOrderSystem.Model.ViewModels.UserProfiles;

namespace BugWorkOrderSystem.IServices
{
    public interface IUserServices : IBaseServices<User>
    {
        Task<MessageModel<UserViewDto>> Login(UserLoginDto user);
        Task<MessageModel<dynamic>> Logout(Guid id);
        Task<MessageModel<UserViewDto>> Update(UserUpdateDto user);
        Task<MessageModel<PageModel<UsersViewDto>>> QueryList(int page, int rows);
        Task<MessageModel<UserViewDto>> Add(UserAddDto add);
        Task<MessageModel<SysUserViewDto>> GetUser(Guid id);
        Task<MessageModel<dynamic>> DeleteUser(Guid id);
        Task<MessageModel<dynamic>> DisableUser(Guid id);
        Task<MessageModel<dynamic>> ResetPassword(Guid id);
    }
}
