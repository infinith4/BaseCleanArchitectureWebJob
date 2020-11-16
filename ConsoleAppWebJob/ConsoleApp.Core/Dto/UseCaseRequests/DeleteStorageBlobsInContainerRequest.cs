using ConsoleApp.Core.Dto.UseCaseResponses;
using ConsoleApp.Core.Interfaces;

namespace ConsoleApp.Core.Dto.UseCaseRequests
{
    public class ExecuteConsoleWriteRequest : IUseCaseRequest<ExecuteConsoleWriteResponse>
    {
        public string OutputStr { get; }

        public ExecuteConsoleWriteRequest(string outputStr)
        {
            OutputStr = outputStr;
        }
    }
}
