﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Api.Core.Dto.GatewayResponses.Repositories;

namespace Web.Api.Core.Interfaces.Gateways.Repositories
{
    public interface IMstAppSettingRepository
    {
        GetAppSettingListResponse GetAppSettingList();
    }
}
