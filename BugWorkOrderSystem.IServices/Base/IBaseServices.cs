using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BugWorkOrderSystem.IServices.Base
{
    public interface IBaseServices<TEntity> where TEntity : class
    {
        Task<TEntity> QueryById(Guid id);

        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> groupExpression);

        Task<TEntity> Query(Expression<Func<TEntity, bool>> expression);
    }
}
