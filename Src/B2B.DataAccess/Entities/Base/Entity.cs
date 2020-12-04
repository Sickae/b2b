using B2B.Shared.Interfaces;
using System;

namespace B2B.DataAccess.Entities.Base
{
    public abstract class Entity : IEntity
    {
        public virtual int Id { get; set; }

        public virtual DateTime CreationDateUTC { get; set; }

        public virtual DateTime ModificationDateUTC { get; set; }

        protected Entity()
        {
            CreationDateUTC = DateTime.UtcNow;
            ModificationDateUTC = DateTime.UtcNow;
        }
    }
}
