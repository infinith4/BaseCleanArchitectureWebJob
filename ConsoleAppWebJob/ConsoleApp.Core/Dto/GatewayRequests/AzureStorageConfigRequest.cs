using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Core.Dto.GatewayRequests
{
    public class AzureStorageConfigRequest
    {
        public string StorageAccountName { get; }
        public string StorageAccountKey { get; }

        public AzureStorageConfigRequest(string storageAccountName, string storageAccountKey)
        {
            StorageAccountName = storageAccountName;
            StorageAccountKey = storageAccountKey;
        }
    }
}
