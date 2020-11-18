using MediatR;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Cache.Commands.DeleteAdUnitIdByGroupIdCache
{
    public class DeleteAdUnitIdByGroupIdCacheCommandHandler : IRequestHandler<DeleteAdUnitIdByGroupIdCacheCommand>
    {
        private readonly IDatabase AdsRedis;

        public DeleteAdUnitIdByGroupIdCacheCommandHandler(
            AdsRedisContext adsRedis)
        {
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(DeleteAdUnitIdByGroupIdCacheCommand request, CancellationToken cancellationToken)
        {
            var redisKey = CacheKey.UnitIdListByGroupId(request.AdGroupId);
            await AdsRedis.KeyDeleteAsync(redisKey, CommandFlags.FireAndForget);

            return Unit.Value;
        }
    }
}
