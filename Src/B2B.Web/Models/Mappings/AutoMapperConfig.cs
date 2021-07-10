using System.Reflection;
using AutoMapper;
using B2B.Logic.Mappings;

namespace B2B.Web.Models.Mappings
{
    public static class AutoMapperConfig
    {
        public static void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.AllowNullCollections = true;
        }

        public static Assembly[] GetAutoMapperProfileAssemblies()
        {
            var assemblies = new[]
            {
                Assembly.GetAssembly(typeof(UserMappings)),
                Assembly.GetAssembly(typeof(ViewModelMappings)),
            };

            return assemblies;
        }
    }
}
