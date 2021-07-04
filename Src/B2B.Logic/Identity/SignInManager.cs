using System.Threading.Tasks;
using AutoMapper;
using B2B.DataAccess.Entities;
using B2B.Logic.BusinessLogic.Base.Service;
using B2B.Shared.Enums;
using B2B.Shared.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NHibernate;

namespace B2B.Logic.Identity
{
    public class SignInManager : SignInManager<AppIdentityUser>
    {
        private readonly LoggingService _loggingService;

        public SignInManager(UserManager<AppIdentityUser> userManager,
            IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<AppIdentityUser> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<AppIdentityUser>> logger,
            IAuthenticationSchemeProvider schemes,
            IUserConfirmation<AppIdentityUser> userConfirmation,
            LoggingService loggingService)
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, userConfirmation)
        {
            _loggingService = loggingService;
        }

        public override Task SignInAsync(AppIdentityUser user, bool isPersistent, string authenticationMethod = null)
        {
            _loggingService.LogOperation(null, LogOperationType.Login, user.Id);
            return base.SignInAsync(user, isPersistent, authenticationMethod);
        }

        public override Task SignOutAsync()
        {
            _loggingService.LogOperation(null, LogOperationType.Logout);
            return base.SignOutAsync();
        }
    }
}
