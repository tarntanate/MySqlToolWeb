using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Cache.AdStatsCache.Commands.DeleteAdStatsCache
{
    public class DeleteAdStatsCacheCommandHandler : IRequestHandler<DeleteAdStatsCacheCommand>
    {
        private readonly IDatabase AdsRedis;

        public DeleteAdStatsCacheCommandHandler(AdsRedisContext adsRedis)
        {
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(DeleteAdStatsCacheCommand request, CancellationToken cancellationToken)
        {
            var redisKey = CacheKey.AdStats(request.AdId);
            await AdsRedis.KeyDeleteAsync(redisKey);
            return Unit.Value;
        }
    }
}
