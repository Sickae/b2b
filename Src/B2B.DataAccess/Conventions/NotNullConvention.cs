using B2B.DataAccess.Attributes;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace B2B.DataAccess.Conventions
{
    public class NotNullConvention : AttributePropertyConvention<NotNullAttribute>
    {
        protected override void Apply(NotNullAttribute attribute, IPropertyInstance instance)
        {
            instance.Not.Nullable();
        }
    }
}