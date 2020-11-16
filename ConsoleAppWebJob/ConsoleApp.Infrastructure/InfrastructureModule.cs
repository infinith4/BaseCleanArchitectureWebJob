using Autofac;
using ConsoleApp.Core.Interfaces.Gateways.Repositories;
using ConsoleApp.Infrastructure.Data.EntityFramework.Repositories;

namespace ConsoleApp.Infrastructure
{
    public class InfrastructureModule : Module
    {
        //NOTE: ここにRepositoryを追加しないと使えない
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MstAppSettingRepository>().As<IMstAppSettingRepository>().InstancePerLifetimeScope();
        }
    }
}
