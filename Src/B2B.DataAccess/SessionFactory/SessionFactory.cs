using B2B.DataAccess.Conventions;
using B2B.DataAccess.Entities.Base;
using B2B.DataAccess.Helpers;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Cfg;
using NHibernate.Dialect;

namespace B2B.DataAccess.SessionFactory
{
    public static class SessionFactory
    {
        public static Configuration BuildConfiguration(string connectionString)
        {
            var config = Fluently.Configure();
            var dbConfiguration = PostgreSQLConfiguration.Standard.ConnectionString(connectionString)
                .Dialect<PostgreSQL83Dialect>()
                .FormatSql()
                .AdoNetBatchSize(100);

#if DEBUG
            dbConfiguration.ShowSql();
#endif

            config = config.Database(dbConfiguration);
            var configuration = SetupConfig(config);

#if DEBUG
            SqlScriptExporter.ExportScripts(configuration, config);
#endif
            return configuration;
        }

        private static Configuration SetupConfig(FluentConfiguration config)
        {
            config = config.Mappings(m => m.AutoMappings.Add(
                AutoMap.AssemblyOf<EntityBase>(new StoreConfiguration())
                    .IgnoreBase<EntityBase>()
                    .IgnoreBase<LogicalEntityBase>()
                    .Conventions.Add<HasManyConvention>()
                    .Conventions.Add<HasManyToManyConvention>()
                    .Conventions.Add<NotNullConvention>()
                    .Conventions.Add<PrimaryKeySequenceConvention>()
                    .Conventions.Add<PropertyConvention>()
                    .Conventions.Add<TableNameConvention>()
                    .Conventions.Add<TextConvention>()
                    .Conventions.Add<UniqueConvention>()
                    .Conventions.Add<UtcDateTimeKindConvention>()
            ));

            var cfg = config.BuildConfiguration();
            cfg.SetProperty("hbm2ddl.keywords", "auto-quote");

            return cfg;
        }
    }
}
