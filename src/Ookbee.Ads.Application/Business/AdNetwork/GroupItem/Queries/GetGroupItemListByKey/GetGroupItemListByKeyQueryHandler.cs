using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Infrastructure;
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

            var redisKey = CacheKey.GroupItemList(request.AdGroupId);
            var redisValue = await AdsRedis.StringGetAsync(redisKey);

            if (redisValue.HasValue)
                return result.Success(redisValue);

            return result.Fail(404, $"AdGroup '{request.AdGroupId}' doesn't exist.");
        }
    }
}
