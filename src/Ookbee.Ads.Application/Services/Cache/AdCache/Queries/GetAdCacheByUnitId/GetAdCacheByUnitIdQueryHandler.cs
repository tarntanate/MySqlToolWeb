using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Cache.AdUnitStatsCache.Commands.IncrementAdUnitStatsCache;
using Ookbee.Ads.Application.Services.Cache.AdUserCache.Queries.IsExistsAdUserCacheById;
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
        private readonly IMediator Mediator;
        private readonly IDatabase AdsRedis;

        public GetAdByUnitIdQueryHandler(
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
        }

        public async Task<Response<string>> Handle(GetAdByUnitIdQuery request, CancellationToken cancellationToken)
        {
            await Mediator.Send(new IncrementAdUnitStatsCacheCommand(AdStatsType.Request, request.AdUnitId), cancellationToken);

            var redisKey = CacheKey.UnitsAdIds(request.AdUnitId, request.Platform);
            var redisValue = string.Empty;

            if (request.UserId.HasValue())
            {
                var isExistsAdUserResponse = await Mediator.Send(new IsExistsAdUserCacheByIdQuery(request.UserId.Value), cancellationToken);
                if (!isExistsAdUserResponse.IsSuccess)
                    redisKey = CacheKey.UnitsAdIdsPreview(request.AdUnitId, request.Platform);
            }

            var setMembers = await AdsRedis.SetMembersAsync(redisKey);
            if (setMembers.HasValue())
            {
                var adIds = setMembers.Select(adId => (long)adId);
                var adId = adIds.OrderBy(adId => Guid.NewGuid()).First();
                redisKey = CacheKey.Ad(adId);
                redisValue = await AdsRedis.HashGetAsync(redisKey, request.Platform.ToString());
            }

            var result = new Response<string>();
            if (redisValue.HasValue())
            {
                await Mediator.Send(new IncrementAdUnitStatsCacheCommand(AdStatsType.Fill, request.AdUnitId));
                return result.OK(redisValue);
            }
            return result.NotFound();
        }
    }
}
