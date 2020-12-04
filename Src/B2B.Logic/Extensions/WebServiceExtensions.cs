using B2B.DataAccess.SessionFactory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace B2B.Logic.Extensions
{
    public static class WebServiceExtensions
    {
        public static void CreateSessionFactory(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("b2b");
            var cfg = SessionFactory.BuildConfiguration(connectionString);
            var sessionFactory = cfg.BuildSessionFactory();
            services.AddSingleton(sessionFactory);
        }
    }
}
