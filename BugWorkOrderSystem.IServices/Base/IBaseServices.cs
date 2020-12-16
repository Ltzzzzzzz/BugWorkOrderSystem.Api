using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BugWorkOrderSystem.Model;

namespace BugWorkOrderSystem.IServices.Base
{
    public interface IBaseServices<TEntity> where TEntity : class
    {
        Task<TEntity> GetById(Guid id);

        Task<List<TEntity>> QueryList(Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, object>> order = null, PageModel<TEntity> page = null);

        Task<int> Add(TEntity entity);

        Task<bool> IsExist(Expression<Func<TEntity, bool>> lambda);

        Task<bool> DeleteByIds(Guid[] ids);

        Task<bool> Update(TEntity entity);
    }
}
