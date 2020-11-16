using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseRequests
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
