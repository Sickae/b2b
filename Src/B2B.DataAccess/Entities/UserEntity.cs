using B2B.DataAccess.Entities.Base;

namespace B2B.DataAccess.Entities
{
    public class UserEntity : LogicalEntityBase
    {
        public UserEntity()
        {
            UserEntity a = null;
            Test(a);
        }

        public virtual string UserName { get; set; }

        public virtual string PasswordHash { get; set; }

        public virtual string InGameName { get; set; }

        private void Test(UserEntity entity)
        {
        }
    }
}
