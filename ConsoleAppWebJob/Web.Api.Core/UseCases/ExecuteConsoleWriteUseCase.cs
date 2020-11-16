using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Web.Api.Core.Dto;
using Web.Api.Core.Dto.GatewayRequests;
using Web.Api.Core.Dto.UseCaseRequests;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Web.Api.Core.Interfaces.UseCases;
using System.Linq;
using Web.Api.Core.Interfaces.Gateways.Repositories.Logs;

namespace Web.Api.Core.UseCases
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
            this._configuration = configuration;
            this._serverLoggerRepository = serverLoggerRepository;
            this._mstAppSettingRepository = mstAppSettingRepository;
        }

        public async Task<bool> Handle(ExecuteConsoleWriteRequest executeConsoleWriteRequest, IOutputPort<ExecuteConsoleWriteResponse> outputPort)
        {
            Type currentType = this.GetType();
            try
            {
                Console.WriteLine("exeute");

                await this._serverLoggerRepository.Info($"Execute ExecuteConsoleWrite.Developmentaaaasdfasfddfas", currentType);
                await this._serverLoggerRepository.Warning($"Execute ExecuteConsoleWrite.Development WARNING", currentType);
                await this._serverLoggerRepository.Error($"Execute ExecuteConsoleWrite.Developmentaaaaasfdasdfasdfa", null, currentType);

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
                await this._serverLoggerRepository.Error($"{Properties.ErrorResources.ErrorOccuredException}", ex, currentType);
                outputPort.Handle(new ExecuteConsoleWriteResponse(new[] { new Error("", "") }));
                return false;
            }
        }
    }
}
