using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Cache.Commands.AdGroupStatsCache;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Infrastructure.Enums;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.AdUnitCache.Commands.GetAdUnitCacheByGroupId
{
    public class GetAdUnitCacheByGroupIdQueryHandler : IRequestHandler<GetAdUnitCacheByGroupIdQuery, HttpResult<string>>
    {
        private IMediator Mediator { get; }
        private IDatabase AdsRedis { get; }

        public GetAdUnitCacheByGroupIdQueryHandler(
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
        }

        public async Task<HttpResult<string>> Handle(GetAdUnitCacheByGroupIdQuery request, CancellationToken cancellationToken)
        {
            await Mediator.Send(new UpdateAdGroupStatsCacheCommand(request.AdGroupId, AdStats.Request), cancellationToken);
            var redisKey = CacheKey.Units(request.AdGroupId);
            var redisValue = await AdsRedis.StringGetAsync(redisKey);

            var result = new HttpResult<string>();
            return result.Success(redisValue);
        }
    }
}
