using System.Collections.Generic;
using ConsoleApp.Core.Interfaces;

namespace ConsoleApp.Core.Dto.UseCaseResponses
{
    public class ExecuteConsoleWriteResponse : UseCaseResponseMessage
    {
        public IEnumerable<Error> Errors { get; }

        public ExecuteConsoleWriteResponse(IEnumerable<Error> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public ExecuteConsoleWriteResponse(bool success = false, string message = null) : base(success, message)
        {
        }
    }
}
