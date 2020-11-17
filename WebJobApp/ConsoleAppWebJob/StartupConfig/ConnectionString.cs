using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace ConsoleAppWebJob.StartupConfig
{
    internal class ConnectionString
    {
        internal static string GetApplicationDbContextConnectionString(string sqlDatabaseUserId, string sqlDatabasePassword, string connectionString)
        {
            connectionString = connectionString.Replace("{SqlDatabaseUserId}", sqlDatabaseUserId);
            return connectionString.Replace("{SqlDatabasePassword}", sqlDatabasePassword);
        }
    }
}
