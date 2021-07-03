using System;

namespace B2B.Shared.Interfaces
{
    public interface IEntity
    {
        int Id { get; }

        DateTime CreationDateUtc { get; }

        DateTime ModificationDateUtc { get; }
    }
}
