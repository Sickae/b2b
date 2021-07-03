using B2B.Shared.Interfaces;

namespace B2B.DataAccess.Entities.Base
{
    public abstract class LogicalEntityBase : EntityBase, ILogicalDeletableEntity
    {
        public virtual bool IsDeleted { get; set; }
    }
}
