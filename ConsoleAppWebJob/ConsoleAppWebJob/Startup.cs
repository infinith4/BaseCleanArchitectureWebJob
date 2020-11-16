using System;
using System.Net;
using System.Reflection;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ConsoleApp.Core;
using ConsoleApp.Infrastructure;
using ConsoleApp.Infrastructure.Data.Entities;
using ConsoleApp.Infrastructure.Data.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace ConsoleAppWebJob
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
        //    var connectionString = StartupConfig.ConnectionString.GetApplicationDbContextConnectionString(
        //        this.Configuration, this.Configuration.GetConnectionString("ApplicationDbContextName")
        //        );
        //    //services.AddDbContext<ZuoraLib.Models.SubscriptionContext>(options =>
        //    //        options.UseSqlServer(connectionString, sqlServerOptionsAction: sqlOptions =>
        //    //        {
        //    //            sqlOptions.EnableRetryOnFailure(
        //    //            maxRetryCount: 5,
        //    //            maxRetryDelay: TimeSpan.FromSeconds(30),
        //    //            errorNumbersToAdd: null);
        //    //        }));
        //    services.AddDbContext<ApplicationDbContext>
        //        (
        //        options =>
        //        options.UseSqlServer(
        //            connectionString,
        //            b => b.MigrationsAssembly("ConsoleApp.Infrastructure")
        //            )
        //    );

            return BuildDependencyInjectionProvider(services);
        }

        private static IServiceProvider BuildDependencyInjectionProvider(IServiceCollection services)
        {
            // Now register our services with Autofac container.
            var builder = new ContainerBuilder();

            builder.RegisterModule(new CoreModule());  //NOTE: UseCaseの追加が必要
            builder.RegisterModule(new InfrastructureModule());  //NOTE: Repositoryの追加が必要

            // Presenters
            //builder.RegisterType<OrderPresenter>().SingleInstance();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).Where(t => t.Name.EndsWith("Presenter")).SingleInstance();

            builder.Populate(services);
            var container = builder.Build();
            // Create the IServiceProvider based on the container.
            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
        }
    }
}
