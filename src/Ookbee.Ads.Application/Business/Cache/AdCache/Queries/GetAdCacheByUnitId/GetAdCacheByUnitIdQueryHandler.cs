using MediatR;
using Ookbee.Ads.Application.Business.Cache.AdUnitStatsCache.Commands.IncrementAdUnitStatsCache;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.AdAssetCache.Commands.GetAdAssetByUnitId
{
    public class GetAdAssetByUnitIdQueryHandler : IRequestHandler<GetAdAssetByUnitIdQuery, HttpResult<string>>
    {
        private IMediator Mediator { get; }
        private IDatabase AdsRedis { get; }

        public GetAdAssetByUnitIdQueryHandler(
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
        }

        public async Task<HttpResult<string>> Handle(GetAdAssetByUnitIdQuery request, CancellationToken cancellationToken)
        {
            await Mediator.Send(new IncrementAdUnitStatsCacheCommand(request.AdUnitId, request.Platform, StatsType.Request), cancellationToken);

            var redisKey = CacheKey.UnitsAdIds(request.AdUnitId, request.Platform);
            var redisValue = string.Empty;
            var redisValues = await AdsRedis.SetMembersAsync(redisKey);
            if (redisValues.HasValue())
            {
                var adIds = redisValues.Select(adId => (long)adId);
                var adId = adIds.OrderBy(adId => Guid.NewGuid()).First();
                redisKey = CacheKey.Ad(adId, request.Platform);
                redisValue = await AdsRedis.StringGetAsync(redisKey);
            }

            var result = new HttpResult<string>();

            if (!redisValue.HasValue())
                return result.Fail(404, "Data not found.");

            await Mediator.Send(new IncrementAdUnitStatsCacheCommand(request.AdUnitId, request.Platform, StatsType.Fill));

            return result.Success(redisValue);
        }
    }
}
