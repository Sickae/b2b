using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using B2B.Logic.Identity;
using B2B.Shared.Interfaces;
using Microsoft.AspNetCore.Http;

namespace B2B.Web.Infrastructure
{
    public class AppContext : IAppContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int? UserId
        {
            get
            {
                var claims = ((ClaimsIdentity) CurrentUser?.Identity)?.Claims;
                var userIdClaim = claims?.FirstOrDefault(x => x.Type == AppClaimTypes.UserId);
                if (userIdClaim == null) return null;

                return int.Parse(userIdClaim.Value);
            }
        }

        public IPrincipal CurrentUser => _httpContextAccessor.HttpContext?.User;
    }
}
