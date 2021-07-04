using Microsoft.AspNetCore.Identity;

namespace B2B.Logic.Identity
{
    public class AppIdentityUser : IdentityUser<int>
    {
        public string InGameName { get; set; }
    }
}
