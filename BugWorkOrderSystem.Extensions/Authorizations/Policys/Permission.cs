using Microsoft.AspNetCore.Authorization;

namespace BugWorkOrderSystem.Common.Policys
{
    public class Permission : IAuthorizationRequirement
    {
        public string Role { get; set; }
    }
}
