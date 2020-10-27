using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace BugWorkOrderSystem.Repository.Base
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {

        Task<TEntity> QueryById(Guid id);

        Task<TEntity> Query(Expression<Func<TEntity, bool>> expression);

        Task<TResult> Query<T, T2, T3, TResult>(
            Expression<Func<T, T2, T3, object[]>> joinExpression,
            Expression<Func<T, T2, T3, TResult>> selectExpression,
            Expression<Func<T, T2, T3, bool>> whereExpression
        ) where T : class;

        Task<List<TResult>> QueryList<T, T2, T3, TResult>(
            Expression<Func<T, T2, T3, object[]>> joinExpression,
            Expression<Func<T, T2, T3, TResult>> selectExpression,
            Expression<Func<T, T2, T3, bool>> whereExpression
        ) where T : class;

        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> groupExpression);

    }
}
