using System.Collections;
using System.Linq;
using B2B.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;

namespace B2B.DatabaseCreator.TestDataSetup
{
    public partial class TestData
    {
        private static readonly IPasswordHasher<UserEntity> PasswordHasher = new PasswordHasher<UserEntity>();

        private UserEntity[] Users { get; set; }

        public ICollection All => new object[]
            {
                Users
            }
            .Where(x => x != null)
            .SelectMany(x => (object[]) x)
            .ToList();

        public virtual void CreateAllTestData()
        {
            CreateUsers();
        }
    }
}
