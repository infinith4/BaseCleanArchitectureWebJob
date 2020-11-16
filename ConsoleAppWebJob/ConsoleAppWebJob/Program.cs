using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Configuration;
using Autofac.Extensions.DependencyInjection;
using Web.Api.Core;
using Web.Api.Core.Dto.UseCaseRequests;
using Web.Api.Core.Interfaces.UseCases;
using Web.Api.Infrastructure;
using Web.Api.Infrastructure.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ConsoleAppWebJob.Interfaces;
using ConsoleAppWebJob.Service;
using StructureMap;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Logging.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using System.Threading;

namespace ConsoleAppWebJob
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var channel = new Microsoft.ApplicationInsights.Channel.InMemoryChannel();

            //https://docs.microsoft.com/ja-jp/azure/azure-monitor/app/asp-net-core
            //TODO: https://docs.microsoft.com/ja-jp/azure/azure-monitor/app/ilogger
            //TelemetryClient _telemetryClient = new TelemetryClient();
            Console.WriteLine("Start App.");
            string basePath = Directory.GetCurrentDirectory();
            string environmentName = Environment.GetEnvironmentVariable(Constants.Environment.AspNetCoreEnvironment);
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", false, true)  //optional: trueは存在していなくても構わない。
                .AddJsonFile($"appsettings.{environmentName}.json", true, true)
                .AddEnvironmentVariables();

            IConfigurationRoot config = configurationBuilder.Build();
            string keyVaultName = config[Constants.Configuration.KeyName.KeyVaultName];
            Console.WriteLine($"KeyVaultName: {keyVaultName}");
            if (!string.IsNullOrEmpty(keyVaultName))
            {
                //ConsoleAppの場合はADを使う
                config = configurationBuilder.AddAzureKeyVault(
                    $"https://{config[Constants.Configuration.KeyName.KeyVaultName]}.vault.azure.net/",
                    config[Constants.Configuration.KeyName.AzureADApplicationId],
                    config[Constants.Configuration.KeyName.AzureADPassword]).Build();
            }

            string instrumentationKey = config["ApplicationInsights:InstrumentationKey"];

            IServiceCollection services = new ServiceCollection();

            services.Configure<TelemetryConfiguration>(conf =>
            {
                conf.TelemetryChannel = channel;
            });

            TelemetryClient telemetryClient = GetApplicationInsightsTelemetryClient(instrumentationKey);

            var factory = new LoggerFactory();
            ILogger logger = factory.CreateLogger("ConsoleAppWebJob");

            services.AddSingleton<IExecuteConsoleWriteService, ExecuteConsoleWriteService>()
                .AddSingleton<IConfiguration>(config)
                .AddSingleton(logger)
                .AddSingleton(telemetryClient); // Flushしなくても出るので、telemetry clientを渡す必要はなし

            services.AddLogging(builder =>
            {
                builder.AddConfiguration(config.GetSection("Logging"));
                builder.AddFilter<ApplicationInsightsLoggerProvider>("ConsoleAppWebJob", LogLevel.Trace);
#if false // ILoggerBuilder経由でconsoleの設定をしても反映されない(consoleやapplication insightsに出ない)ので、下の方でLoggerFactory経由で設定している
                builder.AddApplicationInsights(instrumentationKey);
                builder.AddConsole();
#endif
            });

            string keyVaultSqlDatabaseUserId = config[Constants.Configuration.KeyVault.Secret.SqlDatabaseUserId];
            string keyVaultSqlDatabasePassword = config[Constants.Configuration.KeyVault.Secret.SqlDatabasePassword];
            string connectionString = StartupConfig.ConnectionString.GetApplicationDbContextConnectionString(
                keyVaultSqlDatabaseUserId, keyVaultSqlDatabasePassword, config.GetConnectionString(Constants.ConnectionString.Db.ApplicationDbContextName)
                );

            //NOTE: BuildDependencyInjectionProvider Methodを呼ぶ前に書かないといけない
            services.AddDbContext<ApplicationDbContext>
                (
                options =>
                options.UseSqlServer(
                    connectionString,
                    b => b.MigrationsAssembly("Web.Api.Infrastructure")
                    )
            );

            IServiceProvider serviceProvider = BuildDependencyInjectionProvider(config, services);
            //serviceProvider.GetService<ILogger>().LogInformation("aaaa");

            //configure console logging
            factory.AddApplicationInsights(serviceProvider, LogLevel.Trace);
            factory.AddConsole();

            //////do the hard work here
            ////var ExecuteConsoleWriteService = serviceProvider.GetService<IBarService>();
            ExecuteServiceAsync(serviceProvider).GetAwaiter().GetResult();

            Console.WriteLine("End App.");
        }

        private static TelemetryClient GetApplicationInsightsTelemetryClient(string instrumentationKey)
        {
            return new TelemetryClient()
            {
                InstrumentationKey = instrumentationKey
            };
        }

        private static IServiceProvider BuildDependencyInjectionProvider(IConfigurationRoot config, IServiceCollection services)
        {
            //services.AddSingleton(c => GetApplicationInsightsTelemetryClient(config[Constants.ConfigurationKey.AppSettings.ApplicationInsights.InstrumentationKey]));
            var module = new ConfigurationModule(config);

            // Now register our services with Autofac container.
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule(module);
            containerBuilder.RegisterModule(new CoreModule());  //NOTE: UseCaseの追加が必要
            containerBuilder.RegisterModule(new InfrastructureModule());  //NOTE: Repositoryの追加が必要
            containerBuilder.RegisterModule(new ServerLogger.Infra.InfrastructureModule());  //NOTE: Repositoryの追加が必要

            // Presenters
            //builder.RegisterType<RegisterUserPresenter>().SingleInstance();
            //builder.RegisterType<OrderPresenter>().SingleInstance();
            containerBuilder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).Where(t => t.Name.EndsWith("Presenter")).SingleInstance();

            containerBuilder.Populate(services);
            Autofac.IContainer container = containerBuilder.Build();
            // Create the IServiceProvider based on the container.
            return new AutofacServiceProvider(container);
        }

        private static async Task ExecuteServiceAsync(IServiceProvider serviceProvider)
        {
            IExecuteConsoleWriteService executeConsoleWriteService = serviceProvider.GetService<IExecuteConsoleWriteService>();
            await executeConsoleWriteService.ExecuteConsoleWriteLine();
        }
    }
}
