using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdUnitCache.Commands.GetAdUnitCacheByGroupId
{
    public class GetAdUnitCacheByGroupIdQueryHandler : IRequestHandler<GetAdUnitCacheByGroupIdQuery, HttpResult<string>>
    {
        private IMapper Mapper { get; }
        private IDatabase AdsRedis { get; }

        public GetAdUnitCacheByGroupIdQueryHandler(
            IMapper mapper,
            AdsRedisContext adsRedis)
        {
            Mapper = mapper;
            AdsRedis = adsRedis.Database(0);
        }

        public async Task<HttpResult<string>> Handle(GetAdUnitCacheByGroupIdQuery request, CancellationToken cancellationToken)
        {
            var result = new HttpResult<string>();

            var redisKey = CacheKey.UnitsByGroup(request.AdGroupId);
            var redisValue = await AdsRedis.StringGetAsync(redisKey);
            return result.Success(redisValue);
        }
    }
}
