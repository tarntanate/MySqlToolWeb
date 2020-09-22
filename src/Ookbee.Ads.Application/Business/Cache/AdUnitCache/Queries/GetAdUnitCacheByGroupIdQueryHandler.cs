using MediatR;
using Ookbee.Ads.Application.Business.Cache.AdGroupStatsCache.Commands.IncrementAdGroupStatCache;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.AdUnitCache.Commands.GetAdUnitCacheByGroupId
{
    public class GetAdUnitCacheByGroupIdQueryHandler : IRequestHandler<GetAdUnitCacheByGroupIdQuery, Response<string>>
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

        public async Task<Response<string>> Handle(GetAdUnitCacheByGroupIdQuery request, CancellationToken cancellationToken)
        {
            await Mediator.Send(new IncrementAdGroupStatsCacheCommand(StatsType.Request, request.AdGroupId), cancellationToken);
            var redisKey = CacheKey.Units(request.AdGroupId);
            var hashField = request.Platform.ToString();
            var redisValue = await AdsRedis.HashGetAsync(redisKey, hashField);

            var result = new Response<string>();
            if (redisValue.HasValue)
                return result.Success((string)redisValue);
            return result.Fail(404, "Data not found.");
        }
    }
}
