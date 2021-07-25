using System;
using B2B.DataAccess.Attributes;
using B2B.DataAccess.Entities.Base;

namespace B2B.DataAccess.Entities
{
    [AuditLog]
    public class UserEntity : LogicalEntityBase
    {
        [Unique]
        public virtual string InGameName { get; set; }

        [Text]
        public virtual string ApplicationJson { get; set; }

        public virtual ApplicationFlowEntity ApplicationFlow { get; set; }

        public virtual bool IsActivated { get; set; }

        #region Identity

        [Unique]
        public virtual string UserName { get; set; }

        [SkipLog]
        public virtual string PasswordHash { get; set; }

        [SkipLog]
        public virtual string SecurityStamp { get; set; }

        public virtual bool LockoutEnabled { get; set; }

        public virtual int AccessFailedCount { get; set; }

        public virtual DateTime? LockoutEnd { get; set; }

        #endregion
    }
}
