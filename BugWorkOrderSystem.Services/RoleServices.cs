
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BugWorkOrderSystem.IRepository.Base;
using BugWorkOrderSystem.IServices;
using BugWorkOrderSystem.Model;
using BugWorkOrderSystem.Model.Models;
using BugWorkOrderSystem.Model.ViewModels.RoleProfiles;
using BugWorkOrderSystem.Services.Base;
using SqlSugar;

namespace BugWorkOrderSystem.Services
{
    public class RoleServices : BaseServices<Role>, IRoleServices
    {
        private readonly IMapper mapper;

        public RoleServices(IBaseRepository<Role> dal, IMapper mapper)
        {
            baseDal = dal;
            this.mapper = mapper;
        }
        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <param name="page">页数</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public async Task<MessageModel<List<RoleViewDto>>> QueryList()
        {
            var roles = await baseDal.QueryList(u => !u.IsDeleted, u => new { u.CreateDate, OrderByType.Asc });
            var rvds =  mapper.Map<List<RoleViewDto>>(roles);
            return new MessageModel<List<RoleViewDto>> { Success = true, Message = "获取角色列表成功", Data = rvds };
        }
    }
}
