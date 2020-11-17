using System.Net.Http.Headers;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseRequests
{
    public class ExecuteConsoleWriteRequest : IUseCaseRequest<ExecuteConsoleWriteResponse>
    {
        public ExecuteConsoleWriteRequest()
        {
        }
    }
}
