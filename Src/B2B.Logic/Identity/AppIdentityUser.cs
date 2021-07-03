using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace B2B.Logic.Identity
{
    public class AppIdentityUser : IdentityUser<int>
    {
        public DateTime? LastLoginDate { get; set; }

        public IList<AppIdentityUserClaim> UserClaims { get; set; }
    }
}
