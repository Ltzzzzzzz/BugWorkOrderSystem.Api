using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BugWorkOrderSystem.IRepository.Base;
using BugWorkOrderSystem.IServices.Base;
using BugWorkOrderSystem.Model;

namespace BugWorkOrderSystem.Services.Base
{
    public class BaseServices<TEntity> : IBaseServices<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// 基类，从子类获取
        /// </summary>
        public IBaseRepository<TEntity> baseDal;
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> Add(TEntity entity)
        {
            return await baseDal.Add(entity);
        }
        /// <summary>
        /// 根据Ids删除数据
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> DeleteByIds(Guid[] ids)
        {
            return await baseDal.DeleteByIds(ids);
        }
        /// <summary>
        /// 根据id获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TEntity> GetById(Guid id)
        {
            return await baseDal.GetById(id);
        }
        /// <summary>
        /// 查询数据是否存在
        /// </summary>
        /// <param name="lambda">查询条件</param>
        /// <returns></returns>
        public async Task<bool> IsExist(Expression<Func<TEntity, bool>> lambda)
        {
            return await baseDal.IsExist(lambda);
        }
        /// <summary>
        /// 查询数据集合
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="order">排序</param>
        /// <param name="page">分页</param>
        /// <returns></returns>
        public async Task<List<TEntity>> QueryList(Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, object>> order = null, PageModel<TEntity> page = null)
        {
            return await baseDal.QueryList(where, order, page);
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> Update(TEntity entity)
        {
            return await baseDal.Update(entity);
        }
    }
}
