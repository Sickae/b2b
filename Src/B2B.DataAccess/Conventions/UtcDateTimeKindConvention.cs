using B2B.DataAccess.Attributes;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using NHibernate.Type;

namespace B2B.DataAccess.Conventions
{
    public class UtcDateTimeKindConvention : AttributePropertyConvention<UtcDateTimeKindAttribute>
    {
        protected override void Apply(UtcDateTimeKindAttribute attribute, IPropertyInstance instance)
        {
            instance.CustomType<UtcDateTimeType>();
        }
    }
}
