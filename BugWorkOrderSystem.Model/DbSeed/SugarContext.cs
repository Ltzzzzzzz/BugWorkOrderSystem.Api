using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BugWorkOrderSystem.Common.Helper;
using SqlSugar;

namespace BugWorkOrderSystem.Common.Db
{
    public class SugarContext
    {
        /// <summary>
        /// SqlSugar
        /// </summary>
        private static SqlSugarClient _db;
        public SqlSugarClient Db {
            get { return _db; }
            set { _db = value; }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public SugarContext()
        {
            _db = Init();
        }
        /// <summary>
        /// 实体转数据表
        /// </summary>
        /// <param name="createBackup">是否创建备份</param>
        /// <param name="entitys">实体</param>
        public void EntityToTable(bool createBackup = false,params Type[] entitys)
        {
            Console.WriteLine("Init Tables...");
            var rs = false;
            entitys.ToList().ForEach(t =>
            {
                if (!_db.DbMaintenance.IsAnyTable(t.Name))
                {
                    // 数据表不存在再创建
                    Console.WriteLine("Create Table: " + t.Name);
                    if (createBackup)
                    {
                        _db.CodeFirst.BackupTable().InitTables(t);
                    }
                    else
                    {
                        _db.CodeFirst.InitTables(t);
                    }
                    rs = true;
                }
                else
                {
                    // 数据表存在，列名不存在
                    var fields = t.GetProperties().ToList();
                    // 获取数据表的列名集合
                    var columns =_db.DbMaintenance.GetColumnInfosByTableName(t.Name).ToList();
                    var needUpdate = Check(fields, columns);
                    if (needUpdate)
                    {
                        Console.WriteLine("Update Table: " + t.Name);
                        if (createBackup)
                        {
                            _db.CodeFirst.BackupTable().InitTables(t);
                        }
                        else
                        {
                            _db.CodeFirst.InitTables(t);
                        }
                        rs = true;
                    }
                }
            });
            if (rs)
            {
                Console.WriteLine("Init Tables Finish");
            }
            else
            {
                Console.WriteLine("No Tables Need To Init");
            }
        }
        /// <summary>
        /// 初始化数据库对象
        /// </summary>
        /// <returns>SqlSugarClient</returns>
        protected static SqlSugarClient Init()
        {
            var con = Appsettins.app(new string[] { "SqlServer", "ConnectionStr" });

            return new SqlSugarClient(new ConnectionConfig
            {
                ConnectionString = con,
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            });
        }
        /// <summary>
        /// 检查数据表是否需要更新
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        private static bool Check(List<PropertyInfo> fields, List<DbColumnInfo> columns)
        {
            var fieldNames = fields.Select(f => f.Name);
            var columnNames = columns.Select(c => c.DbColumnName);
            if (fields.Count == columns.Count)
            {
                var rs = Enumerable.Except<string>(fieldNames, columnNames).ToList();
                if (rs.Count != 0)
                {
                    return true;
                }
                return false;
            }
            return true;
        }
    }
}
