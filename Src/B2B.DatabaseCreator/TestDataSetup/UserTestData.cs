using System;
using System.Collections.Generic;
using B2B.DataAccess.Entities;
using B2B.Logic.Identity;
using B2B.Shared.Enums;

namespace B2B.DatabaseCreator.TestDataSetup
{
    public partial class TestData
    {
        protected virtual void CreateUsers()
        {
            Users = new[]
            {
                CreateUser(1, ".Abc12345", "Sickae", "ChelleRae", UserRole.Admin)
            };
        }

        private UserEntity CreateUser(int id, string password, string userName, string inGameName, UserRole role)
        {
            var passwordHash = PasswordHasher.HashPassword(null, password);

            var user = new UserEntity
            {
                UserName = userName,
                PasswordHash = passwordHash,
                SecurityStamp = NewSecurityStamp(),
                InGameName = inGameName,
                IsDeleted = false,
                CreationDateUtc = DateTime.UtcNow,
                ModificationDateUtc = DateTime.UtcNow
            };

            UserClaims ??= new List<UserClaimEntity>();

            UserClaims.Add(CreateUserClaim(user, AppClaimTypes.Role, role.ToString()));
            UserClaims.Add(CreateUserClaim(user, AppClaimTypes.UserId, id.ToString()));

            return user;
        }

        private static UserClaimEntity CreateUserClaim(UserEntity user, string claimType, string claimValue)
        {
            return new()
            {
                User = user,
                ClaimType = claimType,
                ClaimValue = claimValue,
                CreationDateUtc = DateTime.UtcNow,
                ModificationDateUtc = DateTime.UtcNow
            };
        }

        private static string NewSecurityStamp()
        {
            var bytes = new byte[20];
            Rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}
