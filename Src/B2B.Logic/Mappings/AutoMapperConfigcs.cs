using System.Reflection;
using AutoMapper;

namespace B2B.Logic.Mappings
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
            };

            return assemblies;
        }
    }
}
