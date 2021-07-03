using System;
using B2B.Shared.Interfaces;

namespace B2B.DataAccess.Entities.Base
{
    public abstract class EntityBase : IEntity
    {
        protected EntityBase()
        {
            CreationDateUtc = DateTime.UtcNow;
            ModificationDateUtc = DateTime.UtcNow;
        }

        public virtual int Id { get; set; }

        public virtual DateTime CreationDateUtc { get; set; }

        public virtual DateTime ModificationDateUtc { get; set; }
    }
}