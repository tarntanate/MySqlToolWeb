using MediatR;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Cache.Commands.DeleteAdGroupUnitIdKeyCache
{
    public class DeleteAdGroupUnitIdKeyCacheCommandHandler : IRequestHandler<DeleteAdGroupUnitIdKeyCacheCommand>
    {
        private readonly IDatabase AdsRedis;

        public DeleteAdGroupUnitIdKeyCacheCommandHandler(
            AdsRedisContext adsRedis)
        {
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(DeleteAdGroupUnitIdKeyCacheCommand request, CancellationToken cancellationToken)
        {
            var redisKey = CacheKey.AdGroupUnitId(request.AdGroupId);
            await AdsRedis.KeyDeleteAsync(redisKey, CommandFlags.FireAndForget);

            return Unit.Value;
        }
    }
}
