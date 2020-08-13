using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdNetwork.GroupItem.Queries.GetAdGroupItemListByKey
{
    public class GetGroupItemListByKeyQueryHandler : IRequestHandler<GetGroupItemListByKeyQuery, HttpResult<string>>
    {
        private IMapper Mapper { get; }
        private IDatabase AdsRedis { get; }

        public GetGroupItemListByKeyQueryHandler(
            IMapper mapper,
            AdsRedisContext adsRedis)
        {
            Mapper = mapper;
            AdsRedis = adsRedis.Database();
        }

        public async Task<HttpResult<string>> Handle(GetGroupItemListByKeyQuery request, CancellationToken cancellationToken)
        {
            var result = new HttpResult<string>();

            var redisKey = $"AD_GROUP_{request.AdGroupId}_ITEMS";
            var redisValue = await AdsRedis.StringGetAsync(redisKey);

            if (redisValue.HasValue)
                return result.Success(redisValue);

            return result.Fail(404, "Data not found.");
        }

        // public async Task<HttpResult<GroupItemDto>> Handle(GetGroupItemListByKeyQuery request, CancellationToken cancellationToken)
        // {
        //     var result = new HttpResult<GroupItemDto>();

        //     var redisKey = $"AD_GROUP_{request.AdGroupId}_ITEMS";
        //     var redisValue = await AdsRedis.StringGetAsync(redisKey);

        //     if (redisValue.HasValue)
        //     {
        //         var adNetworkGroup = new GroupItemDto();
        //         adNetworkGroup.AdUnits = JsonHelper.Deserialize<IEnumerable<GroupItemUnitDto>>(redisValue);
        //         return result.Success(adNetworkGroup);
        //     }

        //     return result.Fail(404, "Data not found.");
        // }
    }
}
