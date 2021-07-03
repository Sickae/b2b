using System;
using B2B.DataAccess.Entities;

namespace B2B.DatabaseCreator.TestDataSetup
{
    public partial class TestData
    {
        public virtual void CreateUsers()
        {
            Users = new[]
            {
                CreateUser(1, ".Abc12345", "Sickae", "ChelleRae")
            };
        }

        private static UserEntity CreateUser(int id, string password, string userName, string inGameName)
        {
            var passwordHash = PasswordHasher.HashPassword(null, password);

            var user = new UserEntity
            {
                UserName = userName,
                PasswordHash = passwordHash,
                InGameName = inGameName,
                IsDeleted = false,
                CreationDateUtc = DateTime.UtcNow,
                ModificationDateUtc = DateTime.UtcNow
            };

            return user;
        }
    }
}
