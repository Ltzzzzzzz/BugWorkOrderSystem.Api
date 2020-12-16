using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BugWorkOrderSystem.Common.HttpContextUser;
using BugWorkOrderSystem.IRepository.Base;
using BugWorkOrderSystem.IServices;
using BugWorkOrderSystem.Model.Models;
using BugWorkOrderSystem.Model.ViewModels.PageProfiles;
using BugWorkOrderSystem.Services.Base;
using SqlSugar;

namespace BugWorkOrderSystem.Services
{
    public class MenuServices : BaseServices<Menu>, IMenuServices
    {
        private readonly IUser httpUser;
        private readonly IMapper mapper;

        public MenuServices(IBaseRepository<Menu> dal, IUser httpUser, IMapper mapper)
        {
            this.httpUser = httpUser;
            this.mapper = mapper;
            baseDal = dal;
        }
        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="uid">用户Id</param>
        /// <returns></returns>
        public async Task<List<MenuViewDto>> GetMenus()
        {
            // 判断该用户是否admin
            var isAdmin = httpUser.GetClaimValueByType(ClaimTypes.Role).Any(r => r == "Admin");
            if (isAdmin)
            {
                var menus = await baseDal.QueryList(m => !m.IsDeleted);
                menus = FormatMenu(menus);
                return mapper.Map<List<MenuViewDto>>(menus);
            }
            else
            {
                // 不需要权限的菜单
                var menus = await baseDal.QueryList(m => !m.IsAuth && !m.IsDeleted);
                // 权限要求的菜单
                var authMenu = await baseDal.MuchTableQuery<Menu, RoleMenu, UserRole, Menu>(
                    (m, rm, ur) => new object[]
                    {
                    JoinType.Left, rm.MenuId == m.Id,
                    JoinType.Left, ur.RoleId == rm.RoleId
                    },
                    (m, rm, ur) => m,
                    (m, rm, ur) => ur.UserId == httpUser.Id && !m.IsDeleted
                );
                // 合并菜单
                menus.AddRange(authMenu);
                // 格式化菜单
                menus = FormatMenu(menus);
                // 映射菜单
                return mapper.Map<List<MenuViewDto>>(menus);
            }
        }
        /// <summary>
        /// 格式化菜单
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private static List<Menu> FormatMenu(List<Menu> list)
        {
            var menus = new List<Menu>();
            if (list.Count == 0) return menus;
            // 排序
            list = list.OrderBy(m => m.Index).ToList();
            var map = new Dictionary<Guid, Menu>();
            list.ForEach(m => map.Add(m.Id, m));
            list.ForEach(m =>
            {
                if (m.Pid != Guid.Empty)
                {
                    var parent = map[m.Pid];
                    if (parent.Children == null)
                    {
                        parent.Children = new List<Menu>();
                    }
                    parent.Children.Add(m);
                }
                else
                {
                    menus.Add(m);
                }
            });
            return menus;
        }
    }
}
