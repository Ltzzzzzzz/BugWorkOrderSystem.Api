using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BugWorkOrderSystem.Model.Models;

namespace BugWorkOrderSystem.IRepository
{
    public interface IRoleRepository
    {
        Task<string> GetRoleById(Guid id);
        Task<Role> SaveRole(string name);
        Task<List<Role>> GetRoles();
    }
}
