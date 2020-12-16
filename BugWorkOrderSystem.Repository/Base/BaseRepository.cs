using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BugWorkOrderSystem.IRepository.Base;
using BugWorkOrderSystem.IRepository.UnitOfWork;
using BugWorkOrderSystem.Model;
using SqlSugar;

namespace BugWorkOrderSystem.Repository.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        protected SqlSugarClient db;

        public BaseRepository(IUnitOfWork unitOfWork)
        {
            db = unitOfWork.GetSugar();
        }
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task<int> Add(TEntity entity)
        {
            return await db.Insertable<TEntity>(entity).ExecuteCommandAsync();
        }
        /// <summary>
        /// 新增数据集合
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public async Task<int> AddList(List<TEntity> entitys)
        {
            return await db.Insertable<TEntity>(entitys).ExecuteCommandAsync();
        }
        /// <summary>
        /// 根据Ids删除数据
        /// </summary>
        /// <param name="ids">Guid数组</param>
        /// <returns></returns>
        public async Task<bool> DeleteByIds(Guid[] ids)
        {
            return await db.Deleteable<TEntity>().In(ids).ExecuteCommandHasChangeAsync();
        }
        /// <summary>
        /// 根据Id查询
        /// </summary>
        /// <param name="id">实体Id</param>
        /// <returns></returns>
        public async Task<TEntity> GetById(Guid id)
        {
            return await db.Queryable<TEntity>().In(id).FirstAsync();
        }
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public async Task<TEntity> Query(Expression<Func<TEntity, bool>> where)
        {
            return await db.Queryable<TEntity>().FirstAsync(where);
        }
        /// <summary>
        /// 根据表达式查询是否存在
        /// </summary>
        /// <param name="lambda"></param>
        /// <returns></returns>
        public async Task<bool> IsExist(Expression<Func<TEntity, bool>> lambda)
        {
            return await db.Queryable<TEntity>().AnyAsync(lambda);
        }
        /// <summary>
        /// 查询集合
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="order">排序</param>
        /// <param name="page">分页</param>
        /// <returns></returns>
        public async Task<List<TEntity>> QueryList(
            Expression<Func<TEntity, bool>> where = null,
            Expression<Func<TEntity, object>> order = null,
            PageModel<TEntity> page = null)
        {
            if (page == null)
            {
                return await db.Queryable<TEntity>().WhereIF(where != null, where).OrderByIF(order != null, order).ToListAsync();
            }
            RefAsync<int> total = 0;
            var rs = await db.Queryable<TEntity>().WhereIF(where != null, where).OrderByIF(order != null, order).ToPageListAsync(page.Page, page.Rows, total);
            page.DataCount = total.Value;
            page.PageCount = (int)Math.Ceiling(page.DataCount / (double)page.Rows);
            return rs;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task<bool> Update(TEntity entity)
        {
            return await db.Updateable<TEntity>(entity).IgnoreColumns(ignoreAllNullColumns: true).ExecuteCommandHasChangeAsync();
        }
        /// <summary>
        /// 连表查询
        /// </summary>
        /// <typeparam name="T1">表1</typeparam>
        /// <typeparam name="T2">表2</typeparam>
        /// <typeparam name="T3">表3</typeparam>
        /// <typeparam name="TResult">结果</typeparam>
        /// <param name="join">连表方式</param>
        /// <param name="select">选择</param>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public async Task<List<TResult>> MuchTableQuery<T1, T2, T3, TResult>(
            Expression<Func<T1, T2, T3, object[]>> join,
            Expression<Func<T1, T2, T3, TResult>> select,
            Expression<Func<T1, T2, T3, bool>> where = null) where T1 : class, new()
        {
            if (where == null)
            {
                return await db.Queryable(join).Select(select).ToListAsync();
            }
            return await db.Queryable(join).Where(where).Select(select).ToListAsync();
        }
        /// <summary>
        /// 更新对应字段
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="updateColumns">更新等字段</param>
        /// <returns></returns>
        public async Task<bool> Update(TEntity entity, params string[] updateColumns)
        {
            return await db.Updateable<TEntity>(entity).UpdateColumns(updateColumns).ExecuteCommandHasChangeAsync();
        }
    }
}
