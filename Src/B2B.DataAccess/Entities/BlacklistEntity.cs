using B2B.DataAccess.Attributes;
using B2B.DataAccess.Entities.Base;

namespace B2B.DataAccess.Entities
{
    public class BlacklistEntity : EntityBase
    {
        [Unique]
        public virtual string InGameName { get; set; }

        public virtual string Reason { get; set; }

        public virtual UserEntity AddedBy { get; set; }
    }
}
