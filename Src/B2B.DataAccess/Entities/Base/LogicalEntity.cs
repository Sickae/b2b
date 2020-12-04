using B2B.Shared.Interfaces;

namespace B2B.DataAccess.Entities.Base
{
    public abstract class LogicalEntity : Entity, ILogicalDeletableEntity
    {
        public bool IsDeleted { get; set; }
    }
}
