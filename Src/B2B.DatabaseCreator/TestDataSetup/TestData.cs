using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using B2B.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;

namespace B2B.DatabaseCreator.TestDataSetup
{
    public partial class TestData
    {
        private static readonly IPasswordHasher<UserEntity> PasswordHasher = new PasswordHasher<UserEntity>();
        private static readonly RandomNumberGenerator Rng = RandomNumberGenerator.Create();

        private ICollection<UserEntity> Users { get; set; }
        private ICollection<UserClaimEntity> UserClaims { get; set; }

        public ICollection All => new object[]
            {
                Users,
                UserClaims,
            }
            .Where(x => x != null)
            .SelectMany(x => (IEnumerable<object>) x)
            .ToList();

        public virtual void CreateAllTestData()
        {
            CreateUsers();
        }
    }
}
