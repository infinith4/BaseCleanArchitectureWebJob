using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Core.Dto.GatewayResponses.Repositories;
using ConsoleApp.Core.Interfaces.Gateways.Repositories;
using ConsoleApp.Infrastructure.Data.Entities;

namespace ConsoleApp.Infrastructure.Data.EntityFramework.Repositories
{
    internal sealed class MstAppSettingRepository : IMstAppSettingRepository
    {
        //DbContext の生成
        private readonly ApplicationDbContext _dbContext;

        public MstAppSettingRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public GetAppSettingListResponse GetAppSettingList()
        {
            var dateTimeNow = DateTime.Now;
            var mstAppSettingList = (from mst in _dbContext.MST_AppSetting
                                     orderby mst.ApiKey
                                     select new AppSettingInfo(mst.ApiKey)).ToList();

            if (mstAppSettingList.Count() > 0)
            {
                return new GetAppSettingListResponse(mstAppSettingList, success: true);
            }

            return new GetAppSettingListResponse(null);
        }
    }
}
