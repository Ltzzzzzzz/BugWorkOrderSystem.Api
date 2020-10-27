using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace BugWorkOrderSystem.Common.Policys
{
    public class PermissionHandler : AuthorizationHandler<Permission>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, Permission requirement)
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
