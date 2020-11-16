﻿using Autofac;
using Web.Api.Core.Interfaces.UseCases;
using Web.Api.Core.UseCases;

namespace Web.Api.Core
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ExecuteConsoleWriteUseCase>().As<IExecuteConsoleWriteUseCase>().InstancePerLifetimeScope();
        }
    }
}
