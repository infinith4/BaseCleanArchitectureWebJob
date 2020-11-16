using ConsoleApp.Core.Dto.GatewayResponses.Repositories;
using ConsoleApp.Core.Interfaces.Gateways.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using ConsoleApp.Infrastructure.Data.EntityFramework.Repositories;
using Microsoft.Extensions.Configuration;

[assembly: InternalsVisibleTo("ConsoleApp.Infrastructure")]
namespace ConsoleApp.Infrastructure.Tests.Repositories
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
