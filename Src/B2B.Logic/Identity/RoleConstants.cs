using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace B2B.Logic.Identity
{
    public static class RoleConstants
    {
        /// <summary>
        /// Sorted by privileges.
        /// </summary>
        public enum UserRole
        {
            [Description("Admin")] Admin,

            [Description("Manager")] Manager,

            [Description("Member")] Member,

            [Description("Guest")] Guest
        }

        public static IEnumerable<UserRole> GetUserRoles => Enum.GetValues<UserRole>();
    }
}
