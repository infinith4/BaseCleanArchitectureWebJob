using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppWebJob.Interfaces
{
    public interface IExecuteConsoleWriteService
    {
        Task<bool> ExecuteConsoleWriteLine();
    }
}
