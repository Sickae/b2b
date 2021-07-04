using System;
using System.Collections.Generic;
using B2B.Shared.Enums;

namespace B2B.Logic.Identity
{
    public static class RoleConstants
    {
        public const string Admin = nameof(UserRole.Admin);
        public const string Manager = nameof(UserRole.Manager);
        public const string Member = nameof(UserRole.Member);
        public const string Guest = nameof(UserRole.Guest);

        public static IEnumerable<UserRole> GetUserRoles => Enum.GetValues<UserRole>();
    }
}
