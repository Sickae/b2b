﻿using System.Globalization;
using B2B.DataAccess.Helpers;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace B2B.DataAccess.Conventions
{
    public class ReferenceConvention : IReferenceConvention
    {
        public void Apply(IManyToOneInstance instance)
        {
            var entityName = NameConverter.ConvertName(instance.EntityType.Name);
            var columnName = NameConverter.ConvertName(instance.Name);
            var keyName = string.Format("fk_{0}_{1}", columnName, entityName);

            instance.ForeignKey(keyName);
            instance.Column(string.Format(CultureInfo.InvariantCulture, "{0}_id", columnName));
            instance.Index(string.Format(CultureInfo.InvariantCulture, "ix_{0}_{1}_id", entityName, columnName));
        }
    }
}