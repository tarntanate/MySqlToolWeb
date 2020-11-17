using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Redis.AdGroupRedis.Commands.GetAdUnitIdListRedis;
using Ookbee.Ads.Application.Services.Redis.AdRedis.Commands.DeleteAdRedis;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Redis.AdUnitRedis.Commands.DeleteAdUnitRedis
{
    public class DeleteAdUnitRedisCommandHandler : IRequestHandler<DeleteAdUnitRedisCommand>
    {
        private readonly IMediator Mediator;
        private readonly IDatabase AdsRedis;
        private readonly AdsDbRepository<AdUnitEntity> AdUnitDbRepo;

        public DeleteAdUnitRedisCommandHandler(
            IMediator mediator,
            AdsRedisContext adsRedis,
            AdsDbRepository<AdUnitEntity> adUnitDbRepo)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
            AdUnitDbRepo = adUnitDbRepo;
        }

        public async Task<Unit> Handle(DeleteAdUnitRedisCommand request, CancellationToken cancellationToken)
        {
            var getAdUnitIdList = await Mediator.Send(new GetAdUnitIdListRedisQuery(request.AdGroupId), cancellationToken);
            if (getAdUnitIdList.IsSuccess)
            {
                var adUnitIds = getAdUnitIdList.Data;
                foreach (var adUnitId in adUnitIds)
                {
                    var isExists = await AdUnitDbRepo.AnyAsync(
                        filter: f =>
                            f.Id == adUnitId &&
                            f.DeletedAt == null &&
                            f.AdGroup.DeletedAt == null);
                    if (!isExists)
                    {
                        string redisKey;

                        redisKey = RedisKeys.UnitStats(adUnitId);
                        await AdsRedis.KeyDeleteAsync(redisKey);

                        var platforms = EnumHelper.GetValues<AdPlatform>().Where(platform => platform != AdPlatform.Unknown);
                        foreach (var platform in platforms)
                        {
                            redisKey = RedisKeys.UnitAdIds(adUnitId, platform);
                            await AdsRedis.KeyDeleteAsync(redisKey);
                        }

                        redisKey = RedisKeys.UnitIds();
                        await AdsRedis.SetRemoveAsync(redisKey, adUnitId, CommandFlags.FireAndForget);
                    }
                    await Mediator.Send(new DeleteAdRedisCommand(request.CaculatedAt, adUnitId));
                }
            }

            return Unit.Value;
        }
    }
}
