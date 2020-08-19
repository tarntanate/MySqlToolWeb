using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdNetwork.Queries.GetAdUnitListByGroup
{
    public class GetAdUnitListByGroupQueryHandler : IRequestHandler<GetAdUnitListByGroupQuery, HttpResult<string>>
    {
        private IMapper Mapper { get; }
        private AdsRedisContext AdsRedis { get; }

        public GetAdUnitListByGroupQueryHandler(
            IMapper mapper,
            AdsRedisContext adsRedis)
        {
            Mapper = mapper;
            AdsRedis = adsRedis;
        }

        public async Task<HttpResult<string>> Handle(GetAdUnitListByGroupQuery request, CancellationToken cancellationToken)
        {
            var result = new HttpResult<string>();

            var redisKey = CacheKey.UnitByGroup(request.AdGroupId);
            var redisValue = await AdsRedis.Database(0).StringGetAsync(redisKey);

            if (redisValue.HasValue())
                return result.Success(redisValue);
            return result.Fail(404, $"AdGroup '{request.AdGroupId}' doesn't exist.");
        }
    }
}
