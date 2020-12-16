using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BugWorkOrderSystem.Common.Helper;
using BugWorkOrderSystem.Common.HttpContextUser;
using BugWorkOrderSystem.IRepository.Base;
using BugWorkOrderSystem.IServices;
using BugWorkOrderSystem.Model;
using BugWorkOrderSystem.Model.Models;
using BugWorkOrderSystem.Model.ViewModels.RoleProfiles;
using BugWorkOrderSystem.Model.ViewModels.UserProfiles;
using BugWorkOrderSystem.Services.Base;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SqlSugar;

namespace BugWorkOrderSystem.Services
{
    public class UserServices : BaseServices<User> , IUserServices
    {
        private readonly IBaseRepository<UserRole> urDal;
        private readonly IBaseRepository<Role> rDal;
        private readonly IMapper mapper;
        private readonly IUser httpUser;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IConfiguration configuration;

        public UserServices(
            IBaseRepository<User> Dal,
            IBaseRepository<UserRole> urDal,
            IBaseRepository<Role> rDal,
            IMapper mapper,
            IUser httpUser,
            IWebHostEnvironment webHostEnvironment,
            IConfiguration configuration)
        {
            this.urDal = urDal;
            this.rDal = rDal;
            this.mapper = mapper;
            this.httpUser = httpUser;
            this.webHostEnvironment = webHostEnvironment;
            this.configuration = configuration;
            baseDal = Dal;
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="add"></param>
        /// <returns></returns>
        public async Task<MessageModel<UserViewDto>> Add(UserAddDto add)
        {
            var isExist = await baseDal.IsExist(u => u.Account == add.Account);
            if (isExist)
            {
                return new MessageModel<UserViewDto> { Message = "账号已存在" };
            }
            var nameIsExist = await baseDal.IsExist(u => u.Name == add.Name);
            if (nameIsExist)
            {
                return new MessageModel<UserViewDto> { Message = "用户名已存在" };
            }
            var user = mapper.Map<User>(add);
            user.Id = Guid.NewGuid();
            // 创建人
            var cid = httpUser.Id;
            var creator = await baseDal.GetById(cid);
            user.Creator = creator.Name;
            // 插入数据库
            await baseDal.Add(user);
            // 关联权限
            var userRoles = new List<UserRole>();
            var roles = new List<RoleViewDto>();
            add.Roles.ForEach(rid =>
            {
                var role = rDal.GetById(rid).Result;
                var rvd = mapper.Map<RoleViewDto>(role);
                var ur = new UserRole
                {
                    RoleId = rid,
                    UserId = user.Id
                };
                userRoles.Add(ur);
                roles.Add(rvd);
            });
            await urDal.AddList(userRoles);
            var uvd = mapper.Map<UserViewDto>(user);
            uvd.Roles = roles;
            return new MessageModel<UserViewDto> { Message = "添加成功", Success = true, Data = uvd };
        }
        /// <summary>
        /// 根据Id获取用户
        /// </summary>
        /// <param name="id">用户Id</param>
        /// <returns></returns>
        public async Task<MessageModel<SysUserViewDto>> GetUser(Guid id)
        {
            var user = await baseDal.GetById(id);
            var m = new MessageModel<SysUserViewDto> { Message = "用户不存在" };
            if (user!=null)
            {
                var sysUser = mapper.Map<SysUserViewDto>(user);
                // 获取权限组
                var roles = await GetCurrentUserRoles(user.Id);
                var rvd = mapper.Map<List<RoleViewDto>>(roles);
                sysUser.Roles = rvd;
                m.Message = "获取用户成功";
                m.Success = true;
                m.Data = sysUser;
            }
            return m;
        }
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<MessageModel<UserViewDto>> Login(UserLoginDto login)
        {
            var isExist = await baseDal.IsExist(u => u.Account == login.Account && u.IsDeleted);
            if (isExist)
            {
                return new MessageModel<UserViewDto> { Message = "账号不存在" };
            }
            var isDisable = await baseDal.IsExist(u => u.Account == login.Account && u.IsDisable);
            if (isDisable)
            {
                return new MessageModel<UserViewDto> { Message = "账号已停用" };
            }
            // 当前用户
            var currentUser = await baseDal.Query(u => u.Account == login.Account);
            currentUser.LastDate = DateTime.Now;
            await baseDal.Update(currentUser);
            // 尝试登陆时间小于上次登陆时间
            if (currentUser.AgainDate < currentUser.LastDate)
            {
                currentUser.ErrorCount = 0;
                currentUser.AgainDate = currentUser.LastDate;
                await baseDal.Update(currentUser);
            }
            // 账号已登陆
            if (currentUser.Status)
            {
                return new MessageModel<UserViewDto> { Message = "账号已登陆" };
            }
            // 登陆次数=5
            if (currentUser.ErrorCount == 5)
            {
                return new MessageModel<UserViewDto> { Message = "超过尝试次数，请稍后再试" };
            }
            var errPwd = await baseDal.IsExist(u => u.Account == login.Account && u.Password == login.Password);
            if (!errPwd)
            {
                currentUser.ErrorCount += 1;
                currentUser.AgainDate = currentUser.LastDate.AddMinutes(15);
                await baseDal.Update(currentUser);
                return new MessageModel<UserViewDto> { Message = "密码不正确" };
            }
            else
            {
                // 获取权限组
                var roles = await GetCurrentUserRoles(currentUser.Id);
                // 映射
                var uvd = mapper.Map<UserViewDto>(currentUser);
                var rvd = mapper.Map<List<RoleViewDto>>(roles);
                uvd.Roles = rvd;
                currentUser.ErrorCount = 0;
                currentUser.Status = true;
                currentUser.AgainDate = currentUser.LastDate;
                await baseDal.Update(currentUser);
                return new MessageModel<UserViewDto> { Message = "登陆成功", Success = true, Data = uvd };
            }
        }
        /// <summary>
        /// 登出
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<MessageModel<dynamic>> Logout(Guid id)
        {
            var u = await baseDal.GetById(id);
            u.Status = false;
            await baseDal.Update(u);
            return new MessageModel<dynamic> { Message = "已登出", Success = true };
        }
        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <param name="page">分页</param>
        /// <returns></returns>
        public async Task<MessageModel<PageModel<UsersViewDto>>> QueryList(int page, int rows)
        {
            var pm = new PageModel<User>
            {
                Page = page,
                Rows = rows
            };
            var users = await baseDal.QueryList(u => !u.IsDeleted, u => new { u.CreateDate, OrderByType.Asc }, pm);
            // 映射
            var usvd = mapper.Map<IEnumerable<UsersViewDto>>(users);
            usvd.ToList().ForEach(cur =>
            {
                var roles = GetCurrentUserRoles(cur.Id).Result;
                cur.Roles = roles.Select(r => r.Name).ToList();
            });
            var rspm = new PageModel<UsersViewDto>
            {
                Page = pm.Page,
                Rows = pm.Rows,
                PageCount = pm.PageCount,
                DataCount = pm.DataCount,
                Data = usvd
            };
            return new MessageModel<PageModel<UsersViewDto>> { Success = true, Message = "获取用户列表成功", Data = rspm };
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<MessageModel<UserViewDto>> Update(UserUpdateDto update)
        {
            var user = await baseDal.GetById(update.Id);
            var updateColumns = new List<string>();
            if (update.OldPassword == null && update.NewPassword != null)
            {
                return new MessageModel<UserViewDto> { Message = "旧密码不能为空" };
            }
            if (update.OldPassword != null && update.OldPassword != user.Password)
            {
                return new MessageModel<UserViewDto> { Message = "旧密码不正确" };
            }
            else
            {
                if (update.NewPassword != null)
                {
                    updateColumns.Add("Password");
                }
            }
            var updateUser = mapper.Map<User>(update);
            var uvd = mapper.Map<UserViewDto>(update);
            // 修改头像
            if (update.Avatar != null)
            {
                var avatarRes = await WriteAvatar(update.Avatar, update.Id);
                if (avatarRes.Success)
                {
                    updateColumns.Add("Avatar");
                    updateUser.Avatar = avatarRes.Message;
                    uvd.Avatar = updateUser.Avatar;
                }
                else
                {
                    return new MessageModel<UserViewDto> { Message = avatarRes.Message };
                }
            }
            // 更新数据库
            var r = await baseDal.Update(updateUser, updateColumns.ToArray());
            if (r)
            {
                return new MessageModel<UserViewDto> { Message = "更新成功", Success = true, Data = uvd };
            }
            return new MessageModel<UserViewDto> { Message = "更新失败" };
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        public async Task<MessageModel<dynamic>> DeleteUser(Guid id)
        {
            var isInit = await baseDal.IsExist(u => u.Id == id && u.IsInit);
            if (isInit)
            {
                return new MessageModel<dynamic> { Message = "初始用户不能删除" };
            }
            var isOnline = await baseDal.IsExist(u => u.Id == id && u.Status);
            if (isOnline)
            {
                return new MessageModel<dynamic> { Message = "该用户在线不能删除" };
            }
            var user = new User { Id = id, IsDeleted = true };
            var isDelete = await baseDal.Update(user, new string[] { "IsDeleted" });
            if (isDelete)
            {
                return new MessageModel<dynamic> { Message = "删除成功", Success = true };
            }
            return new MessageModel<dynamic> { Message = "删除失败" };
        }
        /// <summary>
        /// 停用用户
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        public async Task<MessageModel<dynamic>> DisableUser(Guid id)
        {
            var isInit = await baseDal.IsExist(u => u.Id == id && u.IsInit);
            if (isInit)
            {
                return new MessageModel<dynamic> { Message = "初始用户不能停用" };
            }
            var isOnline = await baseDal.IsExist(u => u.Id == id && u.Status);
            if (isOnline)
            {
                return new MessageModel<dynamic> { Message = "该用户在线不能停用" };
            }
            var user = new User { Id = id, IsDisable = true };
            var isDisable = await baseDal.Update(user, new string[] { "IsDisable" });
            if (isDisable)
            {
                return new MessageModel<dynamic> { Message = "停用成功", Success = true };
            }
            return new MessageModel<dynamic> { Message = "停用失败" };
        }
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <returns></returns>
        public async Task<MessageModel<dynamic>> ResetPassword(Guid id)
        {
            var isInit = await baseDal.IsExist(u => u.Id == id && u.IsInit);
            if (isInit)
            {
                return new MessageModel<dynamic> { Message = "初始用户不能重置密码" };
            }
            var user = new User { Id = id, Password = MD5Helper.Md5Str("123456") };
            var success = await baseDal.Update(user, new string[] { "Password" });
            if (success)
            {
                return new MessageModel<dynamic> { Message = "密码重置成功，请尽快修改密码", Success = true };
            }
            return new MessageModel<dynamic> { Message = "密码重置失败" };

        }
        /// <summary>
        /// 写入头像
        /// </summary>
        /// <param name="avatar"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        async Task<AvatarRes> WriteAvatar(IFormFile avatar, Guid uid)
        {
            var file = avatar;
            var types = new string[] { "image/jpeg", "image/jpg", "image/png", "image/gif" };
            var avatarRes = new AvatarRes();
            if (!types.Contains(file.ContentType))
            {
                avatarRes.Message = "上传头像图片只能是 JPG/JPEG/PNG/GIF 格式!";
                return avatarRes;
            }
            if (file.Length / 1024 / (double)1024 > 2)
            {
                avatarRes.Message = "上传头像图片大小不能超过 2MB!";
                return avatarRes;
            }
            var basePath = webHostEnvironment.WebRootPath;
            // wwwroot下面的目录
            var avatarPath = Path.Combine(basePath, "avatar");
            // 是否存在avatar文件夹
            if (!FileHelper.IsExistDirectory(avatarPath))
            {
                FileHelper.CreateFolder(avatarPath);
            }
            // 系统下的路径分隔符
            var sep = Path.DirectorySeparatorChar;
            var bytes = FileHelper.FileToBytes(file);
            var type = FileHelper.GetPostfixStr(file.FileName);
            var avatarFilePath = $"{avatarPath}{sep}{uid}{type}";

            await FileHelper.WriteAsync(avatarFilePath, bytes);
            var link = configuration.GetValue(typeof(String), "URLS").ToString() + $"{sep}src{sep}avatar{sep}{uid}{type}".ToString();
            avatarRes.Message = link;
            avatarRes.Success = true;
            return avatarRes;
        }
        /// <summary>
        /// 获取权限组
        /// </summary>
        /// <param name="uid">用户Id</param>
        /// <returns></returns>
        async Task<List<Role>> GetCurrentUserRoles(Guid uid)
        {
            var roles = await baseDal.MuchTableQuery<UserRole, User, Role, Role>(
                        (ur, u, r) => (new object[]
                        {
                            JoinType.Left, ur.UserId == u.Id,
                            JoinType.Left, ur.RoleId == r.Id
                        }),
                        (ur, u, r) => r,
                        (ur, u, r) => u.Id == uid && !r.IsDeleted
                    );
            return roles;
        }
    }
    /// <summary>
    /// 头像响应类
    /// </summary>
    class AvatarRes
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
