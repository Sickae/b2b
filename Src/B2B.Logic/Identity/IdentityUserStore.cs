using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using B2B.Logic.BusinessLogic.User.Command;
using B2B.Logic.BusinessLogic.User.Query;
using B2B.Logic.BusinessLogic.UserClaim.Command;
using B2B.Logic.BusinessLogic.UserClaim.Query;
using B2B.Shared.Dto;
using B2B.Shared.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace B2B.Logic.Identity
{
    public class IdentityUserStore : UserStoreBase<AppIdentityUser, int, AppIdentityUserClaim, IdentityUserLogin<int>
        , IdentityUserToken<int>>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public IdentityUserStore(IMediator mediator, IdentityErrorDescriber identityErrorDescriber, IMapper mapper)
            : base(identityErrorDescriber)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public override IQueryable<AppIdentityUser> Users => throw new NotImplementedException();

        #region User Management

        public override async Task<IdentityResult> CreateAsync(AppIdentityUser identityUser,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (identityUser == null) throw new ArgumentNullException(nameof(identityUser));

            var newUser = new UserDto();
            _mapper.Map(identityUser, newUser);

            var commandResult = await _mediator.Send(new CreateUserCommand {Dto = newUser}, cancellationToken);
            if (!commandResult.Success)
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "user_create_failed",
                    Description = "An error occured while creating a new User."
                });

            newUser.Id = commandResult.NewEntityId;
            _mapper.Map(newUser, identityUser);

            var claims = new List<Claim>
            {
                new AppIdentityUserClaim
                {
                    UserId = identityUser.Id,
                    ClaimType = ClaimTypes.Role,
                    ClaimValue = UserRole.Guest.ToString()
                }.ToClaim(),
                new AppIdentityUserClaim
                {
                    UserId = identityUser.Id,
                    ClaimType = AppClaimTypes.UserId,
                    ClaimValue = identityUser.Id.ToString()
                }.ToClaim()
            };

            var identityResult = await AddClaimsAsync(identityUser, claims, cancellationToken);
            return identityResult;
        }

        public override async Task<IdentityResult> DeleteAsync(AppIdentityUser identityUser,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (identityUser == null) throw new ArgumentNullException(nameof(identityUser));

            var commandResult =  await _mediator.Send(new DeleteUserCommand {Id = identityUser.Id}, cancellationToken);

            return commandResult.Success
                ? IdentityResult.Success
                : IdentityResult.Failed(new IdentityError
                {
                    Code = "delete_user_failed",
                    Description = $"An error occured during deleting User with id {identityUser.Id}"
                });
        }

        public override async Task<AppIdentityUser> FindByIdAsync(string userId,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            var user = await _mediator.Send(new UserByIdQuery {Id = int.Parse(userId)}, cancellationToken);
            return _mapper.Map<AppIdentityUser>(user);
        }

        public override async Task<AppIdentityUser> FindByNameAsync(string normalizedUserName,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            var user = (await _mediator.Send(new UserQuery {UserName = normalizedUserName}, cancellationToken))
                .FirstOrDefault();
            return _mapper.Map<AppIdentityUser>(user);
        }

        public override async Task<AppIdentityUser> FindByEmailAsync(string normalizedEmail,
            CancellationToken cancellationToken = default)
        {
            return await FindByNameAsync(normalizedEmail, cancellationToken);
        }

        protected override async Task<AppIdentityUser> FindUserAsync(int userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            var user = await _mediator.Send(new UserByIdQuery {Id = userId}, cancellationToken);
            return _mapper.Map<AppIdentityUser>(user);
        }

        public override Task<string> GetNormalizedUserNameAsync(AppIdentityUser identityUser,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (identityUser == null) throw new ArgumentNullException(nameof(identityUser));

            return Task.FromResult(identityUser.NormalizedUserName);
        }

        public override Task<string> GetUserIdAsync(AppIdentityUser identityUser,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (identityUser == null) throw new ArgumentNullException(nameof(identityUser));

            return Task.FromResult(identityUser.Id.ToString());
        }

        public override Task<string> GetUserNameAsync(AppIdentityUser identityUser,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (identityUser == null) throw new ArgumentNullException(nameof(identityUser));

            return Task.FromResult(identityUser.UserName);
        }

        public override Task SetNormalizedUserNameAsync(AppIdentityUser identityUser, string normalizedName,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (identityUser == null) throw new ArgumentNullException(nameof(identityUser));

            return Task.FromResult(identityUser.NormalizedUserName = normalizedName);
        }

        public override Task SetUserNameAsync(AppIdentityUser identityUser, string userName,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (identityUser == null) throw new ArgumentNullException(nameof(identityUser));

            return Task.FromResult(identityUser.UserName = userName);
        }

        public override async Task<IdentityResult> UpdateAsync(AppIdentityUser identityUser,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (identityUser == null) throw new ArgumentNullException(nameof(identityUser));

            var user = await _mediator.Send(new UserByIdQuery {Id = identityUser.Id}, cancellationToken);
            _mapper.Map(identityUser, user);

            var commandResult = await _mediator.Send(new UpdateUserCommand {Dto = user}, cancellationToken);

            return commandResult.Success
                ? IdentityResult.Success
                : IdentityResult.Failed(new IdentityError
                {
                    Code = "update_user_failed",
                    Description = $"An error occured while updating User with id {identityUser.Id}"
                });
        }

        #endregion

        #region Password Management

        public override Task<string> GetPasswordHashAsync(AppIdentityUser identityUser,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (identityUser == null) throw new ArgumentNullException(nameof(identityUser));

            return Task.FromResult(identityUser.PasswordHash);
        }

        public override Task<bool> HasPasswordAsync(AppIdentityUser identityUser,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (identityUser == null) throw new ArgumentNullException(nameof(identityUser));

            return Task.FromResult(!string.IsNullOrEmpty(identityUser.PasswordHash) &&
                                   string.IsNullOrWhiteSpace(identityUser.PasswordHash) &&
                                   identityUser.PasswordHash.Length > 0);
        }

        public override Task SetPasswordHashAsync(AppIdentityUser identityUser, string passwordHash,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (identityUser == null) throw new ArgumentNullException(nameof(identityUser));

            return Task.FromResult(identityUser.PasswordHash = passwordHash);
        }

        #endregion

        #region Claim Management

        public override async Task<IList<Claim>> GetClaimsAsync(AppIdentityUser identityUser,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (identityUser == null) throw new ArgumentNullException(nameof(identityUser));

            var claimDtos = await _mediator.Send(new UserClaimQuery
            {
                UserId = identityUser.Id
            }, cancellationToken);
            var claims = claimDtos.Select(x => x.ToClaim()).ToList();

            return claims;
        }

        public override async Task<IdentityResult> AddClaimsAsync(AppIdentityUser identityUser,
            IEnumerable<Claim> claims, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (identityUser == null) throw new ArgumentNullException(nameof(identityUser));
            if (claims == null) throw new ArgumentNullException(nameof(claims));

            var user = await _mediator.Send(new UserByIdQuery {Id = identityUser.Id}, cancellationToken);
            if (user == null)
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "user_not_found", Description = $"User cannot be found with id: {identityUser.Id}"
                });

            foreach (var claim in claims)
            {
                var existingClaims = await _mediator.Send(new UserClaimQuery
                {
                    UserId = identityUser.Id,
                    Type = claim.Type,
                    Value = claim.Value
                }, cancellationToken);

                if (existingClaims.Count == 0)
                {
                    var newClaim = CreateUserClaim(identityUser, claim);
                    var userClaimDto = newClaim.ToClaimDto();

                    var commandResult = await _mediator.Send(new CreateUserClaimCommand {Dto = userClaimDto},
                        cancellationToken);
                    if (!commandResult.Success)
                        return IdentityResult.Failed(new IdentityError
                        {
                            Code = "add_claim_error", Description = "An error occured during creating a user claim."
                        });
                }
            }

            return IdentityResult.Success;
        }

        public override async Task<IdentityResult> ReplaceClaimAsync(AppIdentityUser identityUser, Claim claim,
            Claim newClaim, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (identityUser == null) throw new ArgumentNullException(nameof(identityUser));
            if (claim == null) throw new ArgumentNullException(nameof(claim));
            if (newClaim == null) throw new ArgumentNullException(nameof(newClaim));

            var matchedClaims = await _mediator.Send(new UserClaimQuery
            {
                UserId = identityUser.Id,
                Type = claim.Type,
                Value = claim.Value
            }, cancellationToken);

            foreach (var matchedClaim in matchedClaims)
            {
                matchedClaim.ClaimType = newClaim.Type;
                matchedClaim.ClaimValue = newClaim.Value;

                var commandResult = await _mediator.Send(new UpdateUserClaimCommand {Dto = matchedClaim}, cancellationToken);
                if (!commandResult.Success)
                    return IdentityResult.Failed(new IdentityError
                    {
                        Code = "replace_claim_failed",
                        Description = $"An error happened during replacing Claim with id {matchedClaim.Id}"
                    });
            }

            return IdentityResult.Success;
        }

        public override async Task<IdentityResult> RemoveClaimsAsync(AppIdentityUser identityUser,
            IEnumerable<Claim> claims, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (identityUser == null) throw new ArgumentNullException(nameof(identityUser));
            if (claims == null) throw new ArgumentNullException(nameof(claims));

            foreach (var claim in claims)
            {
                var userClaims = await _mediator.Send(new UserClaimQuery
                {
                    UserId = identityUser.Id,
                    Type = claim.Type,
                    Value = claim.Value
                }, cancellationToken);

                foreach (var userClaim in userClaims)
                {
                    var commandResult = await _mediator.Send(new DeleteUserClaimCommand {Id = userClaim.Id}, cancellationToken);
                    if (!commandResult.Success)
                        return IdentityResult.Failed(new IdentityError
                        {
                            Code = "remove_claim_failed",
                            Description = $"An error happened during removing Claim with id {userClaim.Id}"
                        });
                }

            }

            return IdentityResult.Success;
        }

        public override Task<IList<AppIdentityUser>> GetUsersForClaimAsync(Claim claim,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region External Login Management

        protected override Task<IdentityUserLogin<int>> FindUserLoginAsync(int userId, string loginProvider,
            string providerKey, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        protected override Task<IdentityUserLogin<int>> FindUserLoginAsync(string loginProvider, string providerKey,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public override Task AddLoginAsync(AppIdentityUser user, UserLoginInfo login,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override Task RemoveLoginAsync(AppIdentityUser user, string loginProvider, string providerKey,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override Task<IList<UserLoginInfo>> GetLoginsAsync(AppIdentityUser user,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Token Management

        protected override Task<IdentityUserToken<int>> FindTokenAsync(AppIdentityUser user, string loginProvider,
            string name, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        protected override Task AddUserTokenAsync(IdentityUserToken<int> token)
        {
            throw new NotImplementedException();
        }

        protected override Task RemoveUserTokenAsync(IdentityUserToken<int> token)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
