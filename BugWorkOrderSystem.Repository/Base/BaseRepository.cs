using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BugWorkOrderSystem.Common.Db;
using SqlSugar;

namespace BugWorkOrderSystem.Repository.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        private readonly SugarContext Sugar;

        private readonly SqlSugarClient Db;

        public BaseRepository(SugarContext sugar)
        {
            Sugar = sugar;
            Db = sugar.Db;
        }

        public async Task<TEntity> QueryById(Guid id)
        {
            return await Db.Queryable<TEntity>().In(id).FirstAsync();
        }

        public async Task<TEntity> Query(Expression<Func<TEntity, bool>> expression)
        {
            return await Db.Queryable<TEntity>().FirstAsync(expression);
        }

        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> groupExpression)
        {
            return await Db.Queryable<TEntity>().WhereIF(whereExpression != null, whereExpression).GroupBy(groupExpression).ToListAsync();
        }

        public async Task<List<TResult>> QueryList<T, T2, T3, TResult>(Expression<Func<T, T2, T3, object[]>> joinExpression, Expression<Func<T, T2, T3, TResult>> selectExpression, Expression<Func<T, T2, T3, bool>> whereExpression) where T : class
        {
            return await Db.Queryable(joinExpression).WhereIF(whereExpression != null, whereExpression).Select(selectExpression).ToListAsync();
        }

        public async Task<TResult> Query<T, T2, T3, TResult>(Expression<Func<T, T2, T3, object[]>> joinExpression, Expression<Func<T, T2, T3, TResult>> selectExpression, Expression<Func<T, T2, T3, bool>> whereExpression) where T : class
        {
            return await Db.Queryable(joinExpression).WhereIF(whereExpression != null, whereExpression).Select(selectExpression).FirstAsync();
        }
    }
}
