using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppWebJob.Constants
{
    public class Configuration
    {
        public class KeyVault
        {
            public class Secret
            {
                public const string SqlDatabaseUserId = "SqlDatabaseUserId";
                public const string SqlDatabasePassword = "SqlDatabasePassword";
            }
        }

        internal class KeyName
        {
            internal const string KeyVaultName = "KeyVaultName";
            internal const string AzureADApplicationId = "AzureADApplicationId";
            internal const string AzureADPassword = "AzureADPassword";
            public class ApplicationInsights
            {
                public const string InstrumentationKey = "InstrumentationKey";
            }
        }
    }
}
