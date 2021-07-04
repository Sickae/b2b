using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace B2B.Logic.Identity
{
    public class SignInManager : SignInManager<AppIdentityUser>
    {
        public SignInManager(UserManager<AppIdentityUser> userManager,
            IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<AppIdentityUser> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<AppIdentityUser>> logger,
            IAuthenticationSchemeProvider schemes,
            IUserConfirmation<AppIdentityUser> userConfirmation)
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, userConfirmation)
        {
        }
    }
}
