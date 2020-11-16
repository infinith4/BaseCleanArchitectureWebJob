using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace ConsoleApp.Infrastructure.StartupConfig
{
    internal class ConnectionString
    {
        internal static string GetApplicationDbContextConnectionString(IConfiguration config, string connectionString)
        {
            connectionString = connectionString.Replace("{SqlDatabaseUserId}", config[Constants.Configuration.KeyVault.Secret.SqlDatabaseUserId]);
            return connectionString.Replace("{SqlDatabasePassword}", config[Constants.Configuration.KeyVault.Secret.SqlDatabasePassword]);
        }
    }
}
