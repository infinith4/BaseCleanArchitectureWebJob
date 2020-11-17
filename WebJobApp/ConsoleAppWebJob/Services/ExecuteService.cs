using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Web.Api.Core.Dto.UseCaseRequests;
using Web.Api.Core.Interfaces.UseCases;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ConsoleAppWebJob.Interfaces;

namespace ConsoleAppWebJob.Service
{
    public class ExecuteService : IExecuteService
    {
        private readonly IExecuteConsoleWriteUseCase _executeConsoleWriteUseCase;
        private readonly IConfiguration _configuration;
        //private readonly ILogger _logger;

        public ExecuteService(
            IExecuteConsoleWriteUseCase useCase,
            IConfiguration configuration//,
            //ILogger logger
            )
        {
            this._configuration = configuration;
            this._executeConsoleWriteUseCase = useCase;
            //this._logger = logger;
        }

        public async Task<bool> ExecuteConsoleWriteLine()
        {
            //TODO: responseの修正
            var useCaseRequest = new ExecuteConsoleWriteRequest();

            await this._executeConsoleWriteUseCase.Handle(useCaseRequest, null);
            return false;
        }
    }
}
