using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Cache.AdGroupStatsCache.Commands.IncrementAdGroupStatCache;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Cache.AdUnitCache.Commands.GetAdUnitCacheByGroupId
{
    public class GetAdUnitCacheByGroupIdQueryHandler : IRequestHandler<GetAdUnitCacheByGroupIdQuery, Response<string>>
    {
        private readonly IMediator Mediator;
        private readonly IDatabase AdsRedis;

        public GetAdUnitCacheByGroupIdQueryHandler(
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
        }

        public async Task<Response<string>> Handle(GetAdUnitCacheByGroupIdQuery request, CancellationToken cancellationToken)
        {
            await Mediator.Send(new IncrementAdGroupStatsCacheCommand(AdStatsType.Request, request.AdGroupId), cancellationToken);
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
