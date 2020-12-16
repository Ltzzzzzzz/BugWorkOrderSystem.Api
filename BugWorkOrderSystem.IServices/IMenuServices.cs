using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BugWorkOrderSystem.IServices.Base;
using BugWorkOrderSystem.Model.Models;
using BugWorkOrderSystem.Model.ViewModels.PageProfiles;

namespace BugWorkOrderSystem.IServices
{
    public interface IMenuServices : IBaseServices<Menu>
    {
        Task<List<MenuViewDto>> GetMenus();
    }
}
