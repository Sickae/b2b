using B2B.DataAccess.Attributes;
using B2B.DataAccess.Entities.Base;
using B2B.DataAccess.Helpers;
using FluentNHibernate;
using FluentNHibernate.Automapping;
using System;

namespace B2B.DataAccess.SessionFactory
{
    public class StoreConfiguration : DefaultAutomappingConfiguration
    {
        public override bool ShouldMap(Type type)
        {
            return type.IsSubclassOf(typeof(Entity));
        }

        public override bool IsComponent(Type type)
        {
            return Attribute.IsDefined(type, typeof(ComponentAttribute));
        }

        public override string GetComponentColumnPrefix(Member member)
        {
            var result = NameConverter.ConvertName(member.Name) + "_";
            return result;
        }
    }
}
