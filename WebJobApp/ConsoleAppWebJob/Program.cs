using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Configuration;
using Autofac.Extensions.DependencyInjection;
using Web.Api.Core;
using Web.Api.Infrastructure;
using Web.Api.Infrastructure.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StructureMap;
using Microsoft.Extensions.Logging.ApplicationInsights;

namespace ConsoleAppWebJob
{
    internal class Program
    {
        private static IConfiguration _configuration { get; set; }

        private static async Task Main(string[] args)
        {

            string environmentName = Environment.GetEnvironmentVariable(Constants.Environment.AspNetCoreEnvironment);
            Console.WriteLine($"environmentName: {environmentName}");
            #region 
            IHost host = new HostBuilder()
                .ConfigureAppConfiguration((context, config) =>
                {
                    string basePath = Directory.GetCurrentDirectory();
                    config.SetBasePath(basePath);
                    config.AddJsonFile($"appsettings.json", false, true);
                    config.AddJsonFile($"appsettings.{environmentName}.json", true, true);
                    config.AddEnvironmentVariables();

                    /*
                     * この後 AddAzureKeyVault() を呼ぶ際に指定するパラメータでconfiguration情報が必要なので、
                     * 一旦ConfigurationBuilderをbuildしておく。
                     */
                    IConfigurationRoot builtConfig = config.Build();
                    string keyVaultName = builtConfig[Constants.Configuration.KeyName.KeyVaultName];
                    if (string.IsNullOrEmpty(keyVaultName))
                    {
                        _configuration = builtConfig;
                    }
                    else
                    {
                        string keyVaultUrl = $"https://{keyVaultName}.vault.azure.net/";
                        if (context.HostingEnvironment.IsDevelopment() ||
                        context.HostingEnvironment.EnvironmentName == Constants.Environment.Name.Development)
                        {
                            //直前に一旦buildしているので、AddAzureKeyVault() した結果は、これ以降の context.Configuration
                            //には反映されない。
                            //なので、AddAzureKeyVault() した結果を再度buildして、内部で保持して使用する。
                            _configuration = config.AddAzureKeyVault(
                                keyVaultUrl,
                                builtConfig[Constants.Configuration.KeyName.AzureADApplicationId],
                                builtConfig[Constants.Configuration.KeyName.AzureADPassword]
                                )
                                .Build();
                        }
                        else
                        {
                            _configuration = config.AddAzureKeyVault(keyVaultUrl).Build();
                        }
                    }
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        string connectionString = StartupConfig.ConnectionString.GetApplicationDbContextConnectionString(
                            _configuration[Constants.Configuration.KeyVault.Secret.SqlDatabaseUserId],
                            _configuration[Constants.Configuration.KeyVault.Secret.SqlDatabasePassword],
                            _configuration.GetConnectionString(Constants.ConnectionString.Db.ApplicationDbContextName)
                            );
                        options.UseSqlServer(connectionString, sqlServerOptionsAction: sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly("Web.Api.Infrastructure");
                            sqlOptions.EnableRetryOnFailure(
                                maxRetryCount: 5,
                                maxRetryDelay: TimeSpan.FromSeconds(30),
                                errorNumbersToAdd: null
                                );
                        });
                    });
                })
                .UseServiceProviderFactory<ContainerBuilder>(new AutofacContainerFactory())
                .ConfigureContainer<ContainerBuilder>((context, containerBuilder) =>
                {
                    // Now register our services with Autofac container.
                    containerBuilder.RegisterModule(new ConfigurationModule(_configuration));
                    containerBuilder.RegisterModule(new CoreModule());  //NOTE: UseCaseの追加が必要
                    containerBuilder.RegisterModule(new InfrastructureModule());  //NOTE: Repositoryの追加が必要
                    containerBuilder.RegisterModule(new ServerLogger.Infra.InfrastructureModule());  //NOTE: Repositoryの追加が必要

                    // Presenters
                    containerBuilder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).Where(t => t.Name.EndsWith("Presenter")).SingleInstance();

                })
                .ConfigureWebJobs(b =>
                {
                    b.AddAzureStorageCoreServices();
                    b.AddAzureStorage(a => {
                        a.BatchSize = 1;
                        a.NewBatchThreshold = 0;
                        a.MaxDequeueCount = 1;
                        a.MaxPollingInterval = TimeSpan.FromSeconds(15);
                    });
                })
                .ConfigureLogging((context, b) =>
                {
                    b.AddConfiguration(_configuration.GetSection("Logging"));
                    b.AddFilter<ApplicationInsightsLoggerProvider>("ConsoleAppWebJob", LogLevel.Trace);
                    string instrumentationKey = _configuration[$"{Constants.Configuration.KeyName.ApplicationInsights.ApplicationInsightsStr}:{Constants.Configuration.KeyName.ApplicationInsights.InstrumentationKey}"];
                    b.AddApplicationInsights(instrumentationKey);
                    //b.AddApplicationInsights(o => o.InstrumentationKey = instrumentationKey);
                    b.AddConsole();
                })
                .Build();
            #endregion

            await host.RunAsync();

            //ExecuteServiceAsync(serviceProvider).GetAwaiter().GetResult();

            //Console.WriteLine("End App.");
        }

        // NOTE: MicroBatchFramework(.NET Generic Host)でDIコンテナを差し替える(例としてUnity)
        // https://qiita.com/kwhrkzk/items/810d58f8f0cf1c75af52
        private class AutofacContainerFactory : IServiceProviderFactory<ContainerBuilder>
        {
            private IServiceCollection Services { get; set; }

            public ContainerBuilder CreateBuilder(IServiceCollection services)
            {
                this.Services = services;
                return new ContainerBuilder();
            }

            public IServiceProvider CreateServiceProvider(ContainerBuilder containerBuilder)
            {
                containerBuilder.Populate(this.Services);
                Autofac.IContainer container = containerBuilder.Build();
                return new AutofacServiceProvider(container);
            }
        }

        //private static async Task ExecuteServiceAsync(IServiceProvider serviceProvider)
        //{
        //    var ExecuteConsoleWriteService = serviceProvider.GetService<IExecuteConsoleWriteService>();
        //    await ExecuteConsoleWriteService.ExecuteConsoleWriteLine();
        //}
    }
}
