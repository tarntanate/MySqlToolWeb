using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Redis.AdRedis.Commands.GetAdIdListRedis;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Redis.AdRedis.Commands.DeleteAdRedis
{
    public class DeleteAdRedisCommandHandler : IRequestHandler<DeleteAdRedisCommand>
    {
        private readonly IMediator Mediator;
        private readonly IDatabase AdsRedis;
        private readonly AdsDbRepository<AdEntity> AdDbRepo;

        public DeleteAdRedisCommandHandler(
            IMediator mediator,
            AdsRedisContext adsRedis,
            AdsDbRepository<AdEntity> adDbRepo)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
            AdDbRepo = adDbRepo;
        }

        public async Task<Unit> Handle(DeleteAdRedisCommand request, CancellationToken cancellationToken)
        {
            var getAdIdList = await Mediator.Send(new GetAdIdListRedisCommand(request.AdUnitId), cancellationToken);
            if (getAdIdList.IsSuccess)
            {
                foreach (var adId in getAdIdList.Data)
                {
                    var isExists = await AdDbRepo.AnyAsync(
                        filter: f =>
                            f.Id == adId &&
                            f.DeletedAt == null &&
                            f.AdUnit.DeletedAt == null &&
                            f.AdUnit.AdGroup.DeletedAt == null);
                    if (!isExists)
                    {
                        string redisKey;

                        redisKey = CacheKey.AdPlatforms(adId);
                        await AdsRedis.KeyDeleteAsync(redisKey);

                        redisKey = CacheKey.UnitAdIds(request.AdUnitId);
                        await AdsRedis.SetRemoveAsync(redisKey, adId, CommandFlags.FireAndForget);

                        redisKey = CacheKey.UnitAdIdsPreview(request.AdUnitId);
                        await AdsRedis.SetRemoveAsync(redisKey, adId, CommandFlags.FireAndForget);

                        var platforms = EnumHelper.GetValues<AdPlatform>().Where(platform => platform != AdPlatform.Unknown);
                        foreach (var platform in platforms)
                        {
                            redisKey = CacheKey.UnitAdIds(request.AdUnitId, platform);
                            await AdsRedis.SetRemoveAsync(redisKey, adId, CommandFlags.FireAndForget);
                        
                            redisKey = CacheKey.UnitAdIdsPreview(request.AdUnitId, platform);
                            await AdsRedis.SetRemoveAsync(redisKey, adId, CommandFlags.FireAndForget);
                        }
                        
                        redisKey = CacheKey.AdStats(adId);
                        await AdsRedis.KeyDeleteAsync(redisKey);

                        redisKey = CacheKey.AdIds();
                        await AdsRedis.SetRemoveAsync(redisKey, adId, CommandFlags.FireAndForget);
                    }
                }
            }

            return Unit.Value;
        }
    }
}
