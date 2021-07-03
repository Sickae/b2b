using System.Globalization;
using B2B.DataAccess.Helpers;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace B2B.DataAccess.Conventions
{
    public class PrimaryKeySequenceConvention : IIdConvention
    {
        public void Apply(IIdentityInstance instance)
        {
            var sequenceName = string.Format(CultureInfo.InvariantCulture, "{0}_id_seq",
                NameConverter.ConvertName(instance.EntityType.Name));
            var columnName = string.Format(CultureInfo.InvariantCulture, "{0}",
                NameConverter.ConvertName(instance.Property.Name));

            instance.GeneratedBy.Native(sequenceName);
            instance.Column(columnName);
        }
    }
}