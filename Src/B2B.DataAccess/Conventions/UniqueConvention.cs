using B2B.DataAccess.Attributes;
using B2B.Shared.Interfaces;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace B2B.DataAccess.Conventions
{
    public class UniqueConvention : AttributePropertyConvention<UniqueAttribute>
    {
        protected override void Apply(UniqueAttribute attribute, IPropertyInstance instance)
        {
            if (!typeof(ILogicalDeletableEntity).IsAssignableFrom(instance.EntityType.BaseType))
                instance.Unique();
        }
    }
}