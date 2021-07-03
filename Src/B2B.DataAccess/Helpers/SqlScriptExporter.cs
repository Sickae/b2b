using System;
using System.Collections.Generic;
using System.IO;
using FluentNHibernate.Cfg;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Tool.hbm2ddl;
using Serilog;
using Environment = System.Environment;

namespace B2B.DataAccess.Helpers
{
    public static class SqlScriptExporter
    {
        public static void ExportScripts(Configuration config, FluentConfiguration database)
        {
            try
            {
                var outputFolder = Path.Combine(new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory!)
                    .Parent?.Parent?.Parent?.Parent?.Parent?.FullName ?? string.Empty, "DB");

                if (Directory.Exists(outputFolder))
                {
                    var script = new List<string>(config.GenerateSchemaUpdateScript(new PostgreSQL83Dialect(),
                        new DatabaseMetadata(database.BuildSessionFactory().OpenSession().Connection,
                            new PostgreSQL83Dialect())));

                    File.WriteAllText(Path.Combine(outputFolder, "_UpdateSchema.sql"),
                        string.Join(";" + Environment.NewLine, script));

                    script = new List<string>(config.GenerateSchemaCreationScript(new PostgreSQL83Dialect()));
                    File.WriteAllText(Path.Combine(outputFolder, "_CreateSchema.sql"),
                        string.Join(";" + Environment.NewLine, script));
                }
            }
            catch (Exception e)
            {
                Log.Logger.Error(e, "SessionFactory ExportScripts failed.");
                throw;
            }
        }
    }
}
