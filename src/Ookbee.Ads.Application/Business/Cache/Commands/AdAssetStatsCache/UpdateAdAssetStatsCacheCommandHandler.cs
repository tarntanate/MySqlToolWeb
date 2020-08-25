using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Infrastructure.Enums;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.Commands.AdAssetsStatsCache
{
    public class UpdateAdAssetsStatsCacheCommandHandler : IRequestHandler<UpdateAdAssetsStatsCacheCommand, Unit>
    {
        private IDatabase AdsRedis { get; }

        public UpdateAdAssetsStatsCacheCommandHandler(AdsRedisContext adsRedis)
        {
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(UpdateAdAssetsStatsCacheCommand request, CancellationToken cancellationToken)
        {
            var redisKey = CacheKey.AdStats(request.AdId);
            var hashField = request.Stats.ToString();
            await AdsRedis.HashIncrementAsync(redisKey, hashField);

            return Unit.Value;
        }
    }
}
