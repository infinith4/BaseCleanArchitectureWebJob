using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Core.Dto.UseCaseRequests;
using ConsoleApp.Core.Interfaces.UseCases;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ConsoleAppWebJob.Interfaces;

namespace ConsoleAppWebJob.Service
{
    public class ExecuteConsoleWriteService : IExecuteConsoleWriteService
    {
        private readonly IExecuteConsoleWriteUseCase _ExecuteConsoleWriteUseCase;
        private readonly IConfiguration _configuration;
        //private readonly ILogger _logger;

        public ExecuteConsoleWriteService(
            IExecuteConsoleWriteUseCase ExecuteConsoleWriteUseCase,
            IConfiguration configuration//,
            //ILogger logger
            )
        {
            this._configuration = configuration;
            this._ExecuteConsoleWriteUseCase = ExecuteConsoleWriteUseCase;
            //this._logger = logger;
        }

        public async Task<bool> ExecuteConsoleWriteLine()
        {
            //TODO: responseの修正
            var useCaseRequest = new ExecuteConsoleWriteRequest("outputstr");

            await this._ExecuteConsoleWriteUseCase.Handle(useCaseRequest, null);
            return false;
        }
    }
}
