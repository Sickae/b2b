using B2B.DataAccess.Helpers;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using System.Globalization;

namespace B2B.DataAccess.Conventions
{
    public class TableNameConvention : IClassConvention
    {
        public void Apply(IClassInstance instance)
        {
            instance.Table(string.Format(CultureInfo.InvariantCulture, "{0}", NameConverter.ConvertName(instance.EntityType.Name)));
        }
    }
}
