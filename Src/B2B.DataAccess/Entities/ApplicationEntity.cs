using System;
using B2B.DataAccess.Entities.Base;
using B2B.Shared.Enums;

namespace B2B.DataAccess.Entities
{
    public class ApplicationEntity : EntityBase
    {
        public virtual string InGameName { get; set; }

        public virtual string FormJson { get; set; }

        public virtual ApplicationStatus Status { get; set; }

        public virtual DateTime StatusCompleteDateUtc { get; set; }
    }
}
