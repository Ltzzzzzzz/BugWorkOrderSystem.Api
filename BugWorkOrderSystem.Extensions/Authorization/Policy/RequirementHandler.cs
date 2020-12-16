using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BugWorkOrderSystem.IServices;
using BugWorkOrderSystem.IServices.Base;
using BugWorkOrderSystem.Model.Models;
using Microsoft.AspNetCore.Authorization;

namespace BugWorkOrderSystem.Extensions.Authorization.Policy
{
    public class RequirementHandler : AuthorizationHandler<Requirement>
    {
        private readonly IRoleServices roleServices;

        public RequirementHandler(IRoleServices roleServices)
        {
            this.roleServices = roleServices;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, Requirement requirement)
        {
            var currentUserRoles = context.User.Claims.Where(c => c.Type == requirement.ClaimType).Select(c => c.Value).ToList();

            // 获取所有角色
            if (!requirement.Roles.Any())
            {
                var roles = await roleServices.QueryList(r => !r.IsDeleted);
                requirement.Roles = roles;
            }

            var isMatchRole = requirement.Roles.Any(r => currentUserRoles.Contains(r.Code));

            if (isMatchRole)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            return;
        }
    }
}
