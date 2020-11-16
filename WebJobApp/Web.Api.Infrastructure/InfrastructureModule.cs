using Autofac;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Web.Api.Infrastructure.Data.EntityFramework.Repositories;

namespace Web.Api.Infrastructure
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
