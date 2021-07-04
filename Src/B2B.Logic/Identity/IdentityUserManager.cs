using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using B2B.Logic.BusinessLogic.UserClaim.Query;
using B2B.Shared.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace B2B.Logic.Identity
{
    public class IdentityUserManager : UserManager<AppIdentityUser>
    {
        private readonly IMediator _mediator;

        public IdentityUserManager(IUserStore<AppIdentityUser> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<AppIdentityUser> passwordHasher,
            IEnumerable<IUserValidator<AppIdentityUser>> userValidators,
            IEnumerable<IPasswordValidator<AppIdentityUser>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors, IServiceProvider services,
            ILogger<UserManager<AppIdentityUser>> logger,
            IMediator mediator)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors,
                services, logger)
        {
            _mediator = mediator;
        }

        public async Task ReplaceRoleClaim(AppIdentityUser user, UserRole newRole)
        {
            var userClaims = await _mediator.Send(new UserClaimQuery()
            {
                UserId = user.Id,
                Type = AppClaimTypes.Role
            });

            foreach (var userClaim in userClaims)
                await RemoveClaimAsync(user, userClaim.ToClaim());

            var claim = new AppIdentityUserClaim
            {
                UserId = user.Id,
                ClaimType = AppClaimTypes.Role,
                ClaimValue = newRole.ToString()
            }.ToClaim();
            await AddClaimAsync(user, claim);
        }
    }
}
