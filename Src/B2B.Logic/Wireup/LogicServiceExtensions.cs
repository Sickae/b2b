using B2B.DataAccess.SessionFactory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace B2B.Logic.Wireup
{
    public static class LogicServiceExtensions
    {
        public static void CreateSessionFactory(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("b2b");
            var cfg = SessionFactory.BuildConfiguration(connectionString);
            var sessionFactory = cfg.BuildSessionFactory();
            services.AddSingleton(sessionFactory);
        }

        public static IServiceCollection AddLogicServiceCollection(this IServiceCollection services)
        {
            return services;
        }
    }
}
