using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BugWorkOrderSystem.IServices.Base;
using BugWorkOrderSystem.Model;
using BugWorkOrderSystem.Model.Models;
using BugWorkOrderSystem.Model.ViewModels.RoleProfiles;

namespace BugWorkOrderSystem.IServices
{
    public interface IRoleServices : IBaseServices<Role>
    {
        Task<MessageModel<List<RoleViewDto>>> QueryList();
    }
}
