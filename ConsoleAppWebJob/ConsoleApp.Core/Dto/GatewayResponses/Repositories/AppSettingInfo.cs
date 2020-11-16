using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Core.Dto.GatewayResponses.Repositories
{
    public sealed class AppSettingInfo
    {
        public string ApiKey { get; }

        public AppSettingInfo(string apiKey)
        {
            ApiKey = apiKey;
        }
    }
}
