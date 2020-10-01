using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Redis.AdUnitRedis.Commands.UpdateAdUnitStatsRedis;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System;
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

            var redisKey = CacheKey.UnitAdIds(request.AdUnitId, request.Platform);
            var redisValue = string.Empty;

            var setMembers = await AdsRedis.SetMembersAsync(redisKey);
            if (setMembers.HasValue())
            {
                var adIds = setMembers.Select(adId => (long)adId);
                var adId = adIds.OrderBy(adId => Guid.NewGuid()).First();
                redisKey = CacheKey.AdPlatforms(adId);
                redisValue = await AdsRedis.HashGetAsync(redisKey, request.Platform.ToString());
            }

            var result = new Response<string>();
            if (redisValue.HasValue())
            {
                await Mediator.Send(new UpdateAdUnitStatsRedisCommand(request.AdUnitId, AdStatsType.Fill, 1));
                return result.OK(redisValue);
            }
            return result.NotFound();
        }
    }
}
