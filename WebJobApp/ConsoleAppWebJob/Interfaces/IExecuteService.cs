using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppWebJob.Interfaces
{
    public interface IExecuteService
    {
        Task<bool> ExecuteConsoleWriteLine();
    }
}
