using System.Collections.Generic;
using System.Threading.Tasks;
using ConsoleApp.Core.Dto.GatewayResponses.Repositories;

namespace ConsoleApp.Core.Interfaces.Gateways.Repositories
{
    public interface IMstAppSettingRepository
    {
        GetAppSettingListResponse GetAppSettingList();
    }
}
