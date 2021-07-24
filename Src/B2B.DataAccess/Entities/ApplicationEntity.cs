using B2B.DataAccess.Entities.Base;

namespace B2B.DataAccess.Entities
{
    public class ApplicationEntity : LogicalEntityBase
    {
        public virtual string InGameName { get; set; }

        public virtual string FormJson { get; set; }
    }
}
