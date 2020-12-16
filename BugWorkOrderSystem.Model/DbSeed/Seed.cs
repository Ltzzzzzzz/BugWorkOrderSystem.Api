using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BugWorkOrderSystem.Common.Helper;
using BugWorkOrderSystem.Model.Models;
using SqlSugar;

namespace BugWorkOrderSystem.Model.DBSeed
{
    public class Seed
    {
        /// <summary>
        /// 初始化数据库
        /// </summary>
        /// <param name="sugar"></param>
        /// <returns></returns>
        public async Task Init(SugarContext sugar)
        {
            await Task.Run(() =>
            {
                // 创建数据库
                sugar.Db.DbMaintenance.CreateDatabase();
                // 获取当前项目所有程序集
                var models = Assembly.GetExecutingAssembly().GetTypes();
                // 获取命名空间为xx的程序集
                var entitys = models.Where(m => m.Namespace == "BugWorkOrderSystem.Model.Models").ToArray();
                sugar.EntityToTable(false, entitys);
                // 初始化数据
                CreateSeed(sugar.Db);
            });
        }
        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="db"></param>
        private static void CreateSeed(SqlSugarClient db)
        {
            #region 用户表初始
            if (!db.Queryable<User>().Any(u => u.Account == "Ltzzzzzzz"))
            {
                try
                {
                    Console.WriteLine("创建初始用户");
                    var user = new User()
                    {
                        Id = new Guid("e4c7bd88-e593-4ef3-ab9b-0a4d74b76ec5"),
                        Name = "Ltzzzzzzz",
                        Account = "Ltzzzzzzz",
                        Password = MD5Helper.Md5Str("ltz13823970943"),
                        IsInit = true
                    };
                    db.Insertable<User>(user).ExecuteCommand();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            #endregion

            #region 角色表初始
            if (!db.Queryable<Role>().Any(r=>r.Code == "Admin"))
            {
                try
                {
                    Console.WriteLine("创建Admin");
                    var roles = new List<Role>
                    {
                        new Role()
                        {
                            Name = "管理员",
                            Code = "Admin",
                            Describe = "最高权限",
                            IsInit = true
                        },
                        new Role()
                        {
                            Name = "开发人员",
                            Code = "Developement",
                            Describe = "开发",
                            IsInit = true
                        }
                    };
                    db.Insertable<Role>(roles).ExecuteCommand();
                    Console.WriteLine("初始用户关联Admin权限");
                    var user = db.Queryable<User>().First(u => u.Name == "Ltzzzzzzz");
                    var admin = db.Queryable<Role>().First(r => r.Code == "Admin");
                    var userRoles = new List<UserRole>
                    {
                        new UserRole()
                        {
                            UserId = user.Id,
                            RoleId = admin.Id
                        }
                    };
                    db.Insertable<UserRole>(userRoles).ExecuteCommand();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            #endregion

            #region 菜单表初始
            if (!db.Queryable<RoleMenu>().Any())
            {
                try
                {
                    Console.WriteLine("初始菜单表");
                    Console.WriteLine("初始一级菜单");
                    var menus = new List<Menu>
                    {
                        new Menu()
                        {
                            Name = "首页",
                            Icon = "el-icon-s-home",
                            Module = "home",
                            IsInit = true,
                            Index = 1
                        },
                        new Menu()
                        {
                            Name = "工单项目",
                            Icon = "el-icon-s-platform",
                            Module = "projects",
                            IsInit = true,
                            Index = 2
                        },
                        new Menu()
                        {
                            Name = "用户管理",
                            Icon = "el-icon-user-solid",
                            Module = "users",
                            IsInit = true,
                            IsAuth = true,
                            Index = 97
                        },
                        new Menu()
                        {
                            Name = "权限管理",
                            Icon = "el-icon-s-help",
                            Module = "auth",
                            IsInit = true,
                            IsAuth = true,
                            Index = 98
                        },
                        new Menu()
                        {
                            Name = "系统设置",
                            Icon = "el-icon-s-tools",
                            Module = "system",
                            IsInit = true,
                            Index = 99
                        }
                    };
                    db.Insertable<Menu>(menus).ExecuteCommand();
                    Console.WriteLine("初始权限管理二级菜单");
                    var auth = db.Queryable<Menu>().First(m => m.Name == "权限管理");
                    var authMenus = new List<Menu>
                    {
                        new Menu()
                        {
                            Name = "角色管理",
                            Module = "roles",
                            Pid = auth.Id,
                            IsInit = true,
                            IsAuth = true,
                            Index = 1
                        },
                        new Menu()
                        {
                            Name = "菜单设置",
                            Module = "menus",
                            Pid = auth.Id,
                            IsInit = true,
                            IsAuth = true,
                            Index = 2
                        },
                    };
                    db.Insertable<Menu>(authMenus).ExecuteCommand();
                    Console.WriteLine("权限菜单关联Admin");
                    // 需要关联的菜单
                    var needMenus = db.Queryable<Menu>().Where(m => m.Name == "用户管理" || m.Name == "权限管理" || m.Name == "角色管理" || m.Name == "菜单设置").ToList();

                    var admin = db.Queryable<Role>().First(r => r.Code == "Admin");
                    var roleMenus = new List<RoleMenu>();
                    needMenus.ForEach(m =>
                    {
                        var rm = new RoleMenu
                        {
                            RoleId = admin.Id,
                            MenuId = m.Id
                        };
                        roleMenus.Add(rm);
                    });
                    db.Insertable<RoleMenu>(roleMenus).ExecuteCommand();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            #endregion
        }
    }
}
