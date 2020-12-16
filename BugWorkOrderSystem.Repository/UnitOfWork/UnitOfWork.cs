using System;
using BugWorkOrderSystem.IRepository.UnitOfWork;
using SqlSugar;

namespace BugWorkOrderSystem.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ISqlSugarClient sugar;

        public UnitOfWork(ISqlSugarClient sqlSugarClient)
        {
            // 注入sugar
            sugar = sqlSugarClient;
        }
        /// <summary>
        /// 获取sugar，确保唯一性
        /// </summary>
        /// <returns></returns>
        public SqlSugarClient GetSugar()
        {
            return sugar as SqlSugarClient;
        }
        /// <summary>
        /// 开始事务
        /// </summary>
        public void BeginTran()
        {
            GetSugar().BeginTran();
        }
        /// <summary>
        /// 提交事务
        /// </summary>
        public void CommitTran()
        {
            try
            {
                GetSugar().CommitTran();
            }
            catch (Exception ex)
            {
                GetSugar().RollbackTran();
                throw ex;
            }
        }
        /// <summary>
        /// 回滚事务
        /// </summary>
        public void RollbackTran()
        {
            GetSugar().RollbackTran();
        }
    }
}
