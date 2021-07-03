using AutoMapper;

namespace B2B.DataAccess.Mappings
{
    public static class AutoMapperConfig
    {
        public static void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.AllowNullCollections = true;
        }
    }
}
