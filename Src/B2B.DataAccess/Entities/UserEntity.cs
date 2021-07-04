using System;
using System.Collections.Generic;
using B2B.DataAccess.Attributes;
using B2B.DataAccess.Entities.Base;

namespace B2B.DataAccess.Entities
{
    public class UserEntity : LogicalEntityBase
    {
        [Unique]
        public virtual string InGameName { get; set; }

        #region Identity

        [Unique]
        public virtual string UserName { get; set; }

        public virtual string PasswordHash { get; set; }

        public virtual string SecurityStamp { get; set; }

        public virtual bool LockoutEnabled { get; set; }

        public virtual int AccessFailedCount { get; set; }

        public virtual DateTime? LockoutEnd { get; set; }

        #endregion
    }
}
