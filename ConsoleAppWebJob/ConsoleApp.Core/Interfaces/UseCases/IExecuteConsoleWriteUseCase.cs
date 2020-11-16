using ConsoleApp.Core.Dto.UseCaseRequests;
using ConsoleApp.Core.Dto.UseCaseResponses;

namespace ConsoleApp.Core.Interfaces.UseCases
{
    public interface IExecuteConsoleWriteUseCase : IUseCaseRequestHandler<ExecuteConsoleWriteRequest, ExecuteConsoleWriteResponse>
    {
    }
}
