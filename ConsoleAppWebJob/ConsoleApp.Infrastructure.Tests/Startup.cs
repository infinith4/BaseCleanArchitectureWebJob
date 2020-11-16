using Autofac;
using Autofac.Extensions.DependencyInjection;
using ConsoleApp.Infrastructure.Data.EntityFramework;
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

namespace ConsoleApp.Infrastructure.Tests
{
    public class Startup
    {
        public static IConfigurationRoot Configuration()
        {
        	var environmentName = "Staging";  //Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var basePath = Directory.GetCurrentDirectory();
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json")
                .AddEnvironmentVariables();

            var config = configurationBuilder.Build();

            return config;
        }
    }
}
