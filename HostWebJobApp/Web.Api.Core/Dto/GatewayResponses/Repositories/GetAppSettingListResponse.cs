using System;
using System.Collections.Generic;

namespace Web.Api.Core.Dto.GatewayResponses.Repositories
{
    public sealed class GetAppSettingListResponse : BaseGatewayResponse
    {
        public List<AppSettingInfo> AppSettingInfoList { get; }

        public GetAppSettingListResponse(
            List<AppSettingInfo> appSettingInfoList,
            bool success = false, IEnumerable<Error> errors = null) : base(success, errors)
        {
            AppSettingInfoList = appSettingInfoList;
        }
    }
}
