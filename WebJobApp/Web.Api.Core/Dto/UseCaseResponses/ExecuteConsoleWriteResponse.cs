using System.Collections.Generic;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseResponses
{
    public class ExecuteConsoleWriteResponse : UseCaseResponseMessage
    {
        public IEnumerable<Error> Errors { get; }

        public ExecuteConsoleWriteResponse(IEnumerable<Error> errors, bool success = false, string message = null) : base(success, message)
        {
            this.Errors = errors;
        }
    }
}
