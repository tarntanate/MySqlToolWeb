using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Cache.AdStatsCache.Commands.IncrementAdStatsCache
{
    public class IncrementAdStatsCacheCommandHandler : IRequestHandler<IncrementAdStatsCacheCommand, Response<bool>>
    {
        private readonly IMediator Mediator;
        private IDatabase AdsRedis { get; }

        public IncrementAdStatsCacheCommandHandler(
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
        }

        public async Task<Response<bool>> Handle(IncrementAdStatsCacheCommand request, CancellationToken cancellationToken)
        {
            var redisKey = CacheKey.AdStats(request.AdId);
            var hashEntries = await AdsRedis.HashGetAllAsync(redisKey);
            if (hashEntries.HasValue())
            {
                var quota = (long)hashEntries.SingleOrDefault(hashEntry => hashEntry.Name == StatsType.Quota.ToString()).Value;
                var impression = (long)hashEntries.SingleOrDefault(hashEntry => hashEntry.Name == StatsType.Impression.ToString()).Value;
                if (impression < quota)
                {
                    var hashField = request.StatsType.ToString();
                    await AdsRedis.HashIncrementAsync(redisKey, hashField, 1, CommandFlags.FireAndForget);
                }
                return new Response<bool>().Success(true);
            }
            return new Response<bool>().Fail(404, $"Unable to update stats: Invalid or expired data.");
        }
    }
}
