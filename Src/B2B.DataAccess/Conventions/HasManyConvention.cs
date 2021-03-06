﻿using System.Globalization;
using B2B.DataAccess.Helpers;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace B2B.DataAccess.Conventions
{
    public class HasManyConvention : IHasManyConvention
    {
        public void Apply(IOneToManyCollectionInstance instance)
        {
            instance.Key.Column(string.Format(CultureInfo.InvariantCulture, "{0}_id",
                NameConverter.ConvertName(instance.EntityType.Name)));
            instance.Key.ForeignKey(string.Format(CultureInfo.InvariantCulture, "fk_{0}_{1}",
                NameConverter.ConvertName(instance.Member.Name), NameConverter.ConvertName(instance.EntityType.Name)));
        }
    }
}