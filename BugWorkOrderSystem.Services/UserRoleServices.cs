using System.Threading.Tasks;
using BugWorkOrderSystem.Common.Helper;
using BugWorkOrderSystem.IServices;
using BugWorkOrderSystem.Model;
using BugWorkOrderSystem.Model.Models;
using BugWorkOrderSystem.Model.ViewModels.Users;
using BugWorkOrderSystem.Repository.Base;
using SqlSugar;

namespace BugWorkOrderSystem.Services
{
    public class UserRoleServices : IUserRoleServices
    {
        private readonly IBaseRepository<User> userDal;

        public UserRoleServices(IBaseRepository<User> UserDal)
        {
            userDal = UserDal;
        }

        public async Task<MessageModel<dynamic>> Login(UserLogin login)
        {
            var account = await userDal.Query(x => x.Account == login.Account);
            if (account == null)
            {
                return new MessageModel<dynamic> {
                    Success = false,
                    Message = "用户不存在",
                    Res = null
                };
            }
            // 用户
            var u = await userDal.Query(x => x.Account == login.Account && x.Password == Md5Helper.Md5Str(login.Password));
            if (u == null)
            {
                return new MessageModel<dynamic>
                {
                    Success = false,
                    Message = "密码不正确",
                    Res = null
                };
            }
            // 角色用户关联
            var rs = await userDal.Query<UserRole, User, Role, UserView>(
                (ur, u, r) => new object[] {
                    JoinType.Left, ur.UserId == u.Id,
                    JoinType.Left, ur.RoleId == r.Id
                },
                (ur, u, r) => new UserView
                {
                    Id = u.Id,
                    Name = u.Name,
                    Avatar = u.Avatar,
                    Role = r.Name
                }, null);
            return new MessageModel<object>
            {
                Success = true,
                Message = "登陆成功",
                Res = new {
                    userInfo = rs,
                    token = JwtHelper.IssueJwt(rs.Role)
                }
            };
        }
    }
}
