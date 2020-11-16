using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using ConsoleApp.Core.Dto;
using ConsoleApp.Core.Dto.GatewayRequests;
using ConsoleApp.Core.Dto.UseCaseRequests;
using ConsoleApp.Core.Dto.UseCaseResponses;
using ConsoleApp.Core.Interfaces;
using ConsoleApp.Core.Interfaces.Gateways.Repositories;
using ConsoleApp.Core.Interfaces.UseCases;
using System.Linq;
using ConsoleApp.Core.Interfaces.Gateways.Repositories.Logs;

namespace ConsoleApp.Core.UseCases
{
    public sealed class ExecuteConsoleWriteUseCase : IExecuteConsoleWriteUseCase
    {
        private readonly IConfiguration _configuration;
        private readonly IServerLoggerRepository _serverLoggerRepository;
        private readonly IMstAppSettingRepository _mstAppSettingRepository;
        

        public ExecuteConsoleWriteUseCase(
            IConfiguration configuration,
            IServerLoggerRepository serverLoggerRepository,
            IMstAppSettingRepository mstAppSettingRepository)
        {
            _configuration = configuration;
            _serverLoggerRepository = serverLoggerRepository;
            _mstAppSettingRepository = mstAppSettingRepository;
        }

        public async Task<bool> Handle(ExecuteConsoleWriteRequest executeConsoleWriteRequest, IOutputPort<ExecuteConsoleWriteResponse> outputPort)
        {
            Type currentType = this.GetType();
            try
            {
                Console.WriteLine("exeute");

                await _serverLoggerRepository.Info($"Execute ExecuteConsoleWrite.Developmentaaaasdfasfddfas", currentType);
                await _serverLoggerRepository.Warning($"Execute ExecuteConsoleWrite.Development WARNING", currentType);
                await _serverLoggerRepository.Error($"Execute ExecuteConsoleWrite.Developmentaaaaasfdasdfasdfa", null, currentType);

                //apiKeyの取得
                //var responseGetAppSettingList = _mstAppSettingRepository.GetAppSettingList();

                //if (!responseGetAppSettingList.Success)
                //{
                //    return false;
                //}

                //outputPort.Handle(new ExecuteConsoleWriteResponse(new[] { new Error("", "") })); // ToDo: 上位がoutputPortを指定すること
                return true;
            }
            catch (Exception ex)
            {
                await _serverLoggerRepository.Error($"{Properties.ErrorResources.ErrorOccuredException}", ex, currentType);
                outputPort.Handle(new ExecuteConsoleWriteResponse(new[] { new Error("", "") }));
                return false;
            }
        }
    }
}
