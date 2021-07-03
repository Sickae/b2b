using B2B.DataAccess.Attributes;
using B2B.DataAccess.Entities.Base;

namespace B2B.DataAccess.Entities
{
    public class UserEntity : LogicalEntityBase
    {
        [Unique]
        public virtual string UserName { get; set; }

        public virtual string PasswordHash { get; set; }

        [Unique]
        public virtual string InGameName { get; set; }

        private void Test(UserEntity entity)
        {
        }
    }
}
