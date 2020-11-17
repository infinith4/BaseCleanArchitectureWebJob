using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Api.Core.Dto.GatewayRequests
{
    public class AzureStorageConfigRequest
    {
        public string StorageAccountName { get; }
        public string StorageAccountKey { get; }

        public AzureStorageConfigRequest(string storageAccountName, string storageAccountKey)
        {
            this.StorageAccountName = storageAccountName;
            this.StorageAccountKey = storageAccountKey;
        }
    }
}
