using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BugWorkOrderSystem.Model;

namespace BugWorkOrderSystem.IRepository.Base
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetById(Guid id);

        Task<TEntity> Query(Expression<Func<TEntity, bool>> where);

        Task<List<TEntity>> QueryList(Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, object>> order = null, PageModel<TEntity> page = null);

        Task<int> Add(TEntity entity);

        Task<int> AddList(List<TEntity> entitys);

        Task<bool> IsExist(Expression<Func<TEntity, bool>> lambda);

        Task<bool> DeleteByIds(Guid[] ids);

        Task<bool> Update(TEntity entity);

        Task<bool> Update(TEntity entity, params string[] updateColumns);

        Task<List<TResult>> MuchTableQuery<T1, T2, T3, TResult>(
            Expression<Func<T1, T2, T3, object[]>> join,
            Expression<Func<T1, T2, T3, TResult>> select,
            Expression<Func<T1, T2, T3, bool>> where = null
        ) where T1 : class, new();
    }
}
