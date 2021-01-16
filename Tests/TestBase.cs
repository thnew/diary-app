using System;
using System.IO;
using System.Reflection;
using Api;
using Api.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Tests
{
    /// <summary>
    /// A base class for all further test classes, to provide some basic properties
    /// </summary>
    public abstract class TestBase
    {
        private readonly Lazy<ServiceProvider> _lazyServiceProvider = new Lazy<ServiceProvider>(GetServiceProvider);
        protected ServiceProvider ServiceProvider => _lazyServiceProvider.Value;
        protected DatabaseContext DatabaseContext => ServiceProvider.GetService<DatabaseContext>();

        private static ServiceProvider GetServiceProvider()
        {
            var services = new ServiceCollection();
            var assembly = Assembly.GetExecutingAssembly();
            var fi = new FileInfo(assembly.Location);
            var configPathForTests = Path.Combine(fi.Directory.FullName, "appsettings.json");
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(configPathForTests, optional: true)
                .Build();

            services.AddDbContext<DatabaseContext>(options =>
            {
                options
                    .UseInMemoryDatabase(HistoryRepository.DefaultTableName)
                    .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));
                return;
            });

            Startup.AddServices(services);
            return services.BuildServiceProvider();
        }
    }
}
