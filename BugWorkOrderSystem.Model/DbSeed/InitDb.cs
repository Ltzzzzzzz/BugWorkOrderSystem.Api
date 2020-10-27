using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BugWorkOrderSystem.Common.Db;
using BugWorkOrderSystem.Common.Helper;
using BugWorkOrderSystem.Model.Models;

namespace BugWorkOrderSystem.Model.DbSeed
{
    public class InitDb
    {
        /// <summary>
        /// 实体创建数据表
        /// </summary>
        /// <param name="sugar"></param>
        public async Task InitAsync(SugarContext sugar)
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
                InitData(sugar);
            });
        }
        private static void InitData(SugarContext sugar)
        {
            var db = sugar.Db;
            #region 角色表初始SuperAdmin
            if (!db.Queryable<Role>().Any(u => u.Name == "SuperAdmin"))
            {
                try
                {
                    Console.WriteLine("创建SuperAdmin");
                    var super = new Role()
                    {
                        Name = "SuperAdmin"
                    };
                    db.Insertable<Role>(super).ExecuteCommand();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            #endregion
            #region 用户表初始
            if (!db.Queryable<User>().Any(u => u.Name == "Ltzzzzzzz"))
            {
                try
                {
                    Console.WriteLine("创建初始用户");
                    var user = new User()
                    {
                        Name = "Ltzzzzzzz",
                        Account = "Ltzzzzzzz",
                        Password = Md5Helper.Md5Str("ltz13823970943")
                    };
                    db.Insertable<User>(user).ExecuteCommand();
                    Console.WriteLine("初始用户绑定SuperAdmin");
                    var userRole = new UserRole()
                    {
                        UserId = user.Id,
                        RoleId = db.Queryable<Role>().First(r => r.Name == "SuperAdmin").Id,
                    };
                    db.Insertable<UserRole>(userRole).ExecuteCommand();
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
