using System;

namespace B2B.Shared.Interfaces
{
    public interface IEntity
    {
        int Id { get; }

        DateTime CreationDateUTC { get; }

        DateTime ModificationDateUTC { get; }
    }
}
