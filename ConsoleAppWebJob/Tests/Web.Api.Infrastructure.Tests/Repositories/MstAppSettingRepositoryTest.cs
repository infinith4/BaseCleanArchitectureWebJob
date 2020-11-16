using Web.Api.Core.Dto.GatewayResponses.Repositories;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using Web.Api.Infrastructure.Data.EntityFramework.Repositories;
using Microsoft.Extensions.Configuration;

[assembly: InternalsVisibleTo("Web.Api.Infrastructure")]
namespace Web.Api.Infrastructure.Tests.Repositories
{
    [TestClass]
    public class MstAppSettingRepositoryTest
    {
        private IConfiguration _config;

        [TestInitialize]
        public void StoreEntryInit()
        {
            _config = Startup.Configuration();
        }
    }
}
