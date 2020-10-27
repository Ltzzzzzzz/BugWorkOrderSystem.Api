using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BugWorkOrderSystem.IServices.Base;
using BugWorkOrderSystem.Repository.Base;

namespace BugWorkOrderSystem.Services.Base
{
    public class BaseServices<TEntity> : IBaseServices<TEntity> where TEntity : class
    {
        private readonly IBaseRepository<TEntity> dal;

        public BaseServices(IBaseRepository<TEntity> Dal)
        {
            dal = Dal;
        }

        public Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> groupExpression)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> Query(Expression<Func<TEntity, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> QueryById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
