using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Web.Api.Core.Dto.UseCaseRequests;
using Web.Api.Core.Interfaces.UseCases;
using Microsoft.Extensions.Configuration;
using ConsoleAppWebJob.Interfaces;

namespace ConsoleAppWebJob.Service
{
    public class ConfigurationService
    {
        private readonly IConfiguration _configuration;

        public ConfigurationService(
            IConfiguration configuration
            )
        {
            this._configuration = configuration;
        }
    }
}
