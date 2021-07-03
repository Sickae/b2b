using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using AutoMapper;
using B2B.DataAccess.Mapping;
using B2B.DataAccess.Mappings;
using B2B.DataAccess.SessionFactory;
using B2B.DatabaseCreator.TestDataSetup;
using B2B.Logic.Wireup;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;

namespace B2B.DatabaseCreator
{
    public class Program
    {
        private static IConfigurationRoot _config;
        private static string _connectionString;
        private static IServiceProvider _serviceProvider;

        public static int Main(string[] args)
        {
            try
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\"))
                    .AddJsonFile("appsettings.json", false)
                    .AddJsonFile("appsettings.Development.json", true)
                    .AddJsonFile("appsettings.Production.json", true);

                _config = builder.Build();
                _serviceProvider = CreateServiceProvider(_config);
                _connectionString = _config.GetConnectionString("b2b");

                return Create();
            }
            catch (Exception ex)
            {
                var fc = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;

                Console.Error.WriteLine("Error:");
                Console.Error.WriteLine(ex);

                Console.ForegroundColor = fc;

                if (Debugger.IsAttached)
                    Console.ReadKey();

                return 1;
            }
        }

        private static int Create()
        {
            var configuration = SessionFactory.BuildConfiguration(_connectionString);

            Console.WriteLine("Creating database...");
            CreateDatabase(_connectionString, _config);

            var sessionFactory = configuration.BuildSessionFactory();
            using var session = sessionFactory.OpenSession();

            Console.WriteLine("Creating test data...");

            var testData = new TestData();
            testData.CreateAllTestData();

            Console.WriteLine("Saving data...");
            CreateTestData(session, testData);

            return 0;
        }

        private static IDictionary<string, string> ParseConnectionString(string connectionString)
        {
            return connectionString
                .Split(';', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Split('=', 2))
                .ToDictionary(x => x[0], y => y[1]);
        }

        private static void CreateDatabase(string connectionString, IConfigurationRoot configRoot)
        {
            var csParts = ParseConnectionString(connectionString);
            CreateDatabase(csParts, configRoot);
        }

        private static void CreateDatabase(IDictionary<string, string> connection, IConfigurationRoot configRoot)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            if (configRoot == null) throw new ArgumentNullException(nameof(configRoot));

            var host = connection["Host"];
            var port = connection["Port"];
            var dbName = connection["Database"];
            var user = connection["User ID"];
            var password = connection["Password"];

            RunBatchFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configRoot["CreateDbBatPath"]),
                $"{host} {port} {dbName} {user} {password}");
        }
        private static void RunBatchFile(string batchFilePath, string arguments)
        {
            var isDebugging = Debugger.IsAttached;
            var processInfo = new ProcessStartInfo("cmd.exe", $"/c \"{batchFilePath}\" {arguments}")
            {
                WorkingDirectory = Path.GetDirectoryName(batchFilePath)!,
                UseShellExecute = false,
                RedirectStandardOutput = isDebugging,
                RedirectStandardError = isDebugging
            };

            var process = Process.Start(processInfo);
            if (process == null) return;

            if (isDebugging)
            {
                var output = process.StandardOutput.ReadToEnd();
                Debug.WriteLine("Output:");
                Debug.WriteLine(output);

                var err = process.StandardError.ReadToEnd();
                Debug.WriteLine("Error:");
                Debug.WriteLine(err);
            }

            process.WaitForExit();
            process.Close();
        }

        private static void CreateTestData(ISession session, TestData testData)
        {
            using var transaction = session.BeginTransaction();
            foreach (var data in testData.All)
                session.Save(data);

            transaction.Commit();
        }

        private static IServiceProvider CreateServiceProvider(IConfiguration configuration)
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddLogicServiceCollection();
            serviceCollection.AddAutoMapper(AutoMapperConfig.Configure, GetAutoMapperProfileAssemblies());

            return serviceCollection.BuildServiceProvider();
        }

        private static Assembly[] GetAutoMapperProfileAssemblies()
        {
            var assemblies = new[]
            {
                Assembly.GetAssembly(typeof(UserMappings))
            };

            return assemblies;
        }
    }
}
