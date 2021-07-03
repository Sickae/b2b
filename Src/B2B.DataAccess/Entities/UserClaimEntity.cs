using B2B.DataAccess.Entities.Base;

namespace B2B.DataAccess.Entities
{
    public class UserClaimEntity : EntityBase
    {
        public virtual string ClaimType { get; set; }

        public virtual string ClaimValue { get; set; }

        public virtual UserEntity User { get; set; }
    }
}
