using Autofac;
using ConsoleApp.Core.Interfaces.UseCases;
using ConsoleApp.Core.UseCases;

namespace ConsoleApp.Core
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ExecuteConsoleWriteUseCase>().As<IExecuteConsoleWriteUseCase>().InstancePerLifetimeScope();
        }
    }
}
