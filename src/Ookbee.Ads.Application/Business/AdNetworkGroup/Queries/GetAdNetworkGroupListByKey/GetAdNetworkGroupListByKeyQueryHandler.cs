using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdNetworkGroup.Queries.GetAdAdNetworkGroupListByKey
{
    public class GetAdNetworkGroupListByKeyQueryHandler : IRequestHandler<GetAdNetworkGroupListByKeyQuery, HttpResult<string>>
    {
        private IDatabase AdsRedis { get; }
        private IMapper Mapper { get; }

        public GetAdNetworkGroupListByKeyQueryHandler(
            IMapper mapper,
            AdsRedisContext adsRedis)
        {
            Mapper = mapper;
            AdsRedis = adsRedis.Database();
        }

        public async Task<HttpResult<string>> Handle(GetAdNetworkGroupListByKeyQuery request, CancellationToken cancellationToken)
        {
            var result = new HttpResult<string>();

            var redisKey = CacheKey.UnitByGroup(request.AdGroupId);
            var redisValue = await AdsRedis.StringGetAsync(redisKey);

            if (redisValue.HasValue)
                return result.Success(redisValue);

            return result.Fail(404, $"AdGroup '{request.AdGroupId}' doesn't exist.");
        }
    }
}
