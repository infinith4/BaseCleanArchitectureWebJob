using Autofac;
using Autofac.Extensions.DependencyInjection;
using Web.Api.Infrastructure.Data.EntityFramework;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ConsoleAppWebJob")]

namespace Web.Api.Infrastructure.Tests
{
    public class Startup
    {
        public static IConfigurationRoot Configuration()
        {
            string environmentName = "Staging";  //Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            string basePath = Directory.GetCurrentDirectory();
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json")
                .AddEnvironmentVariables();

            IConfigurationRoot config = configurationBuilder.Build();

            return config;
        }
    }
}
