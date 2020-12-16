using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SqlSugar;

namespace BugWorkOrderSystem.Model.DBSeed
{
    public class SugarContext
    {
        private SqlSugarClient db;
        /// <summary>
        /// 外部获取Db
        /// </summary>
        public SqlSugarClient Db
        {
            get { return db; }
            set { db = value; }
        }
        /// <summary>
        /// 构造函数注入Sqlsugar
        /// </summary>
        /// <param name="sqlSugar"></param>
        public SugarContext(ISqlSugarClient sqlSugar)
        {
            db = sqlSugar as SqlSugarClient;
        }
        /// <summary>
        /// 实体转数据表
        /// </summary>
        public void EntityToTable(bool createBackup = false, params Type[] entitys)
        {
            Console.WriteLine("Init Tables...");
            var rs = false;
            entitys.ToList().ForEach(t =>
            {
                if (!db.DbMaintenance.IsAnyTable(t.Name))
                {
                    // 数据表不存在再创建
                    Console.WriteLine("Create Table: " + t.Name);
                    if (createBackup)
                    {
                        db.CodeFirst.BackupTable().InitTables(t);
                    }
                    else
                    {
                        db.CodeFirst.InitTables(t);
                    }
                    rs = true;
                }
                else
                {
                    // 数据表存在，列名不存在
                    var fields = t.GetProperties().ToList();
                    // 获取数据表的列名集合
                    var columns = db.DbMaintenance.GetColumnInfosByTableName(t.Name).ToList();
                    var needUpdate = Check(fields, columns);
                    if (needUpdate)
                    {
                        Console.WriteLine("Update Table: " + t.Name);
                        if (createBackup)
                        {
                            db.CodeFirst.BackupTable().InitTables(t);
                        }
                        else
                        {
                            db.CodeFirst.InitTables(t);
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
