using MediatR;
using Ookbee.Ads.Application.Services.Cache.AdUnitStatsCache.Commands.IncrementAdUnitStatsCache;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Cache.AdCache.Commands.GetAdByUnitId
{
    public class GetAdByUnitIdQueryHandler : IRequestHandler<GetAdByUnitIdQuery, Response<string>>
    {
        private IMediator Mediator { get; }
        private IDatabase AdsRedis { get; }

        public GetAdByUnitIdQueryHandler(
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
        }

        public async Task<Response<string>> Handle(GetAdByUnitIdQuery request, CancellationToken cancellationToken)
        {
            await Mediator.Send(new IncrementAdUnitStatsCacheCommand(StatsType.Request, request.AdUnitId), cancellationToken);

            var redisKey = CacheKey.UnitsAdIds(request.AdUnitId, request.Platform);
            var redisValue = string.Empty;
            var redisValues = await AdsRedis.SetMembersAsync(redisKey);
            if (redisValues.HasValue())
            {
                var adIds = redisValues.Select(adId => (long)adId);
                var adId = adIds.OrderBy(adId => Guid.NewGuid()).First();
                redisKey = CacheKey.Ad(adId);
                var hashField = request.Platform.ToString();
                redisValue = await AdsRedis.HashGetAsync(redisKey, hashField);
            }

            var result = new Response<string>();

            if (!redisValue.HasValue())
                return result.Fail(404, "Data not found.");

            await Mediator.Send(new IncrementAdUnitStatsCacheCommand(StatsType.Fill, request.AdUnitId));

            return result.Success(redisValue);
        }
    }
}
