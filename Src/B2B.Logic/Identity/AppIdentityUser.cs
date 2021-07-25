using Microsoft.AspNetCore.Identity;

namespace B2B.Logic.Identity
{
    public class AppIdentityUser : IdentityUser<int>
    {
        public string InGameName { get; set; }

        public string ApplicationJson { get; set; }

        public bool IsActivated { get; set; }

        public int ApplicationFlowId { get; set; }
    }
}
