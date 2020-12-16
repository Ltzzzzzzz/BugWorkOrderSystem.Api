using System;
using SqlSugar;

namespace BugWorkOrderSystem.IRepository.UnitOfWork
{
    public interface IUnitOfWork
    {
        SqlSugarClient GetSugar();
        // 开始事务
        void BeginTran();
        // 提交事务
        void CommitTran();
        // 回滚
        void RollbackTran();
    }
}
