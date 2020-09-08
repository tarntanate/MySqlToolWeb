using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.AdAssetStatsCache.Commands.CreateAdAssetStatsCache
{
    public class CreateAdAssetStatsCacheCommandHandler : IRequestHandler<CreateAdAssetStatsCacheCommand>
    {
        private IDatabase AdsRedis { get; }

        public CreateAdAssetStatsCacheCommandHandler(AdsRedisContext adsRedis)
        {
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(CreateAdAssetStatsCacheCommand request, CancellationToken cancellationToken)
        {
            var redisKey = CacheKey.AdStats(request.AdId, request.Platform);
            var hashField = request.Stats.ToString();
            var hashValue = request.Value;
            await AdsRedis.HashSetAsync(redisKey, hashField, hashValue);
            return Unit.Value;
        }
    }
}
