using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace BugWorkOrderSystem.Common.HttpContextUser
{
    public interface IUser
    {
        Guid Id { get; }
        IEnumerable<Claim> GetClaimsIdentity();
        List<string> GetClaimValueByType(string ClaimType);
    }
}
