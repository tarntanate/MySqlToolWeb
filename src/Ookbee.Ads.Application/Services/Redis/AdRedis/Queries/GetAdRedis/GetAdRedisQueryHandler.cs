using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Redis.AdUnitRedis.Commands.UpdateAdUnitStatsRedis;
using Ookbee.Ads.Application.Services.Redis.AdUserRedis.Commands.IsExistsAdUserPreviewRedis;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Cache.AdRedis.Commands.GetAdRedis
{
    public class GetAdRedisQueryHandler : IRequestHandler<GetAdRedisQuery, Response<string>>
    {
        private readonly IMediator Mediator;
        private readonly IDatabase AdsRedis;

        public GetAdRedisQueryHandler(
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
        }

        public async Task<Response<string>> Handle(GetAdRedisQuery request, CancellationToken cancellationToken)
        {
            await Mediator.Send(new UpdateAdUnitStatsRedisCommand(request.AdUnitId, AdStatsType.Request, 1), cancellationToken);

            var adId = (long?)null;
            var redisValue = (RedisValue?)null;

            if (request.UserId != null)
            {
                var isExistsAdUserPreview = await Mediator.Send(new IsExistsAdUserPreviewRedisQuery(request.UserId.Value), cancellationToken);
                if (isExistsAdUserPreview.IsSuccess)
                {
                    var redisKey = CacheKey.UnitAdIdsPreview(request.AdUnitId, request.Platform);
                    adId = (long?)await AdsRedis.SetRandomMemberAsync(redisKey);
                }
            }

            if (!adId.HasValue())
            {
                var redisKey = CacheKey.UnitAdFillRate(request.AdUnitId);
                var hashEntries = await AdsRedis.HashGetAllAsync(redisKey);
                if (hashEntries.HasValue())
                {
                    var elements = hashEntries.Select(x => new KeyValuePair<long, double>((long)x.Name, (double)x.Value)).ToList();
                    var r = new Random();
                    var diceRoll = r.NextDouble() * 100;
                    var cumulative = 0.0;
                    foreach (var element in elements)
                    {
                        cumulative += element.Value;
                        if (diceRoll < cumulative)
                        {
                            adId = element.Key;
                            break;
                        }
                    }
                }
            }

            if (adId.HasValue())
            {
                var redisKey = CacheKey.AdPlatforms(adId.Value);
                redisValue = await AdsRedis.HashGetAsync(redisKey, request.Platform.ToString());
                if (redisValue.HasValue())
                {
                    await Mediator.Send(new UpdateAdUnitStatsRedisCommand(request.AdUnitId, AdStatsType.Fill, 1));
                }
            }

            var result = new Response<string>();
            return redisValue.HasValue()
                ? result.OK(redisValue)
                : result.NotFound();
        }

        private long? RandomElementsBasedOnProbability(ICollection<KeyValuePair<long, double>> elements)
        {

            return null;
        }
    }
}
