using B2B.DataAccess.Attributes;
using B2B.DataAccess.Entities.Base;
using B2B.Shared.Enums;

namespace B2B.DataAccess.Entities
{
    public class AuditLogEntity : EntityBase
    {
        public virtual UserEntity User { get; set; }

        public virtual int? RelatedEntityId { get; set; }

        public virtual LogOperationType OperationType { get; set; }

        [Text]
        public virtual string RelatedEntityJson { get; set; }
    }
}
