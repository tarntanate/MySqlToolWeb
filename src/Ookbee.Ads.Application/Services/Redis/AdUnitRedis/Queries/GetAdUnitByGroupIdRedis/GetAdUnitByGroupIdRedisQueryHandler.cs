using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Cache.AdGroupRedis.Commands.UpdateAdGroupStatsRedis;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Cache.AdUnitRedis.Commands.GetAdUnitByGroupIdRedis
{
    public class GetAdUnitByGroupIdRedisQueryHandler : IRequestHandler<GetAdUnitByGroupIdRedisQuery, Response<string>>
    {
        private readonly IMediator Mediator;
        private readonly IDatabase AdsRedis;

        public GetAdUnitByGroupIdRedisQueryHandler(
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
        }

        public async Task<Response<string>> Handle(GetAdUnitByGroupIdRedisQuery request, CancellationToken cancellationToken)
        {
            await Mediator.Send(new UpdateAdGroupStatsRedisCommand(request.AdGroupId, AdStatsType.Request, 1), cancellationToken);
            
            var redisKey = CacheKey.GroupUnitPlatforms(request.AdGroupId);
            var hashField = request.Platform.ToString();
            var redisValue = await AdsRedis.HashGetAsync(redisKey, hashField);

            var result = new Response<string>();
            if (redisValue.HasValue)
                return result.OK((string)redisValue);
            return result.NotFound();
        }
    }
}
