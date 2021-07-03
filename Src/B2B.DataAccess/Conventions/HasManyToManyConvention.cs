using System.Globalization;
using B2B.DataAccess.Helpers;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace B2B.DataAccess.Conventions
{
    public class HasManyToManyConvention : IHasManyToManyConvention
    {
        public void Apply(IManyToManyCollectionInstance instance)
        {
            var memberName = NameConverter.ConvertName(instance.Member.Name);
            var entityName = NameConverter.ConvertName(instance.EntityType.Name);
            var childTypeName = NameConverter.ConvertName(instance.ChildType.Name);

            var tableName = string.CompareOrdinal(entityName, childTypeName) < 0
                ? string.Format(CultureInfo.InvariantCulture, "{0}_{1}", entityName, childTypeName)
                : string.Format(CultureInfo.InvariantCulture, "{0}_{1}", childTypeName, entityName);
            var keyName = string.Format(CultureInfo.InvariantCulture, "fk_{0}_{1}", memberName, entityName);
            var otherKeyName = string.Format(CultureInfo.InvariantCulture, "fk_{0}_{1}", entityName, memberName);
            var columnName = string.Format(CultureInfo.InvariantCulture, "{0}_id", entityName);
            var otherColumnName = string.Format(CultureInfo.InvariantCulture, "{0}_id", childTypeName);

            instance.Table(tableName);
            instance.Key.Column(columnName);
            instance.Key.ForeignKey(keyName);
            instance.Relationship.Column(otherColumnName);
            instance.Relationship.ForeignKey(otherKeyName);
        }
    }
}