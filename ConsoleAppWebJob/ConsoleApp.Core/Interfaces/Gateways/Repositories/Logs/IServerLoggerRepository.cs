using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Core.Interfaces.Gateways.Repositories.Logs
{
    public interface IServerLoggerRepository
    {
        Task Info(string message, Type type = null);
        Task Warning(string message, Type type = null);
        Task Error(string message, Exception exception = null, Type type = null);
    }
}
