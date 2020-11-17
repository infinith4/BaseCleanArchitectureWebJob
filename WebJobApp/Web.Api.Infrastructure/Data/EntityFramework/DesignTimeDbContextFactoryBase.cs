using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Web.Api.Infrastructure.Helpers;

namespace Web.Api.Infrastructure.Data.EntityFramework
{
    public abstract class DesignTimeDbContextFactoryBase<TContext> :
        IDesignTimeDbContextFactory<TContext> where TContext : DbContext
    {
        public TContext CreateDbContext(string[] args)
        {
            return Create(
                Directory.GetCurrentDirectory(),
                Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
        }
        protected abstract TContext CreateNewInstance(
            DbContextOptions<TContext> options);

        public TContext Create()
        {
            var environmentName =
                Environment.GetEnvironmentVariable(
                    "ASPNETCORE_ENVIRONMENT");

            var basePath = AppContext.BaseDirectory;

            return Create(basePath, environmentName);
        }

        private TContext Create(string basePath, string environmentName)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json", true)
                .AddEnvironmentVariables();

            var config = builder.Build();

            Console.WriteLine($"{Constants.Configuration.KeyName.KeyVaultName}: {config[Constants.Configuration.KeyName.KeyVaultName]}");
            if (environmentName == "Development" || environmentName == Constants.Environment.Name.LocalDevelopment)
            {
                config = builder.AddAzureKeyVault(
                    $"https://{config[Constants.Configuration.KeyName.KeyVaultName]}.vault.azure.net/",
                    config[Constants.Configuration.KeyName.AzureADApplicationId],
                    config[Constants.Configuration.KeyName.AzureADPassword])
                    .Build();
            }

            var dbContextName = Constants.DbContext.ApplicationDbContextName;
            var connstr = StartupConfig.ConnectionString.GetApplicationDbContextConnectionString(
                config, config.GetConnectionString(dbContextName)
                );

            Console.WriteLine($"connstr: {connstr}");
            if (string.IsNullOrWhiteSpace(connstr))
            {
                throw new InvalidOperationException(
                    $"Could not find a connection string named '{dbContextName}'.");
            }

            return this.Create(connstr);
        }

        private TContext Create(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException(
             $"{nameof(connectionString)} is null or empty.",
             nameof(connectionString));

            var optionsBuilder = new DbContextOptionsBuilder<TContext>();

            Console.WriteLine("DesignTimeDbContextFactory.Create(string): Connection string: {0}", connectionString);

            optionsBuilder.UseSqlServer(connectionString);

            var options = optionsBuilder.Options;
            return CreateNewInstance(options);
        }
    }
}
