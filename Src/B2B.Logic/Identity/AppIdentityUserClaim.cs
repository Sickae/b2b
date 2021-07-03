﻿using System.Security.Claims;
using B2B.Shared.Dto;
using Microsoft.AspNetCore.Identity;

namespace B2B.Logic.Identity
{
    public class AppIdentityUserClaim : IdentityUserClaim<int>
    {
        public AppIdentityUserClaim()
        { }

        public AppIdentityUserClaim(AppIdentityUser user, RoleConstants.UserRole role)
        {
            UserId = user.Id;
            ClaimType = ClaimTypes.Role;
            ClaimValue = role.ToString();
        }

        public UserClaimDto ToClaimDto()
        {
            return new UserClaimDto()
            {
                Id = Id,
                UserId = UserId,
                ClaimType = ClaimType,
                ClaimValue = ClaimValue
            };
        }
    }
}
