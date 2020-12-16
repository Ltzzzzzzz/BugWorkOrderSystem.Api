using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace BugWorkOrderSystem.Common.HttpContextUser
{
    public class AspNetUser : IUser
    {
        private readonly IHttpContextAccessor accessor;

        public AspNetUser(IHttpContextAccessor accessor)
        {
            this.accessor = accessor;
        }

        public Guid Id => new Guid(GetClaimValueByType("jti").FirstOrDefault());

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return accessor.HttpContext.User.Claims;
        }

        public List<string> GetClaimValueByType(string ClaimType)
        {
            return GetClaimsIdentity().Where(x => x.Type == ClaimType).Select(x => x.Value).ToList();
        }
    }
}
