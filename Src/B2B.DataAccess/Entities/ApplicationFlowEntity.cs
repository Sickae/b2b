using B2B.DataAccess.Attributes;
using B2B.DataAccess.Entities.Base;

namespace B2B.DataAccess.Entities
{
    public class ApplicationFlowEntity : EntityBase
    {
        [Text]
        public virtual string Description { get; set; }

        [Text]
        public virtual string DescriptionJson { get; set; }
    }
}
