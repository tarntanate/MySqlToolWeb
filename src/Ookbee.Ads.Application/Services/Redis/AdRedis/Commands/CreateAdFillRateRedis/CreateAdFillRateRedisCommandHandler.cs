using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Analytics.AdStat.Queries.GetAdStatsList;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Redis.AdRedis.Commands.CreateAdFillRateRedis
{
    public class CreateAdFillRateRedisCommandHandler : IRequestHandler<CreateAdFillRateRedisCommand>
    {
        private readonly IMediator Mediator;
        private readonly IDatabase AdsRedis;
        private readonly AdsDbRepository<AdGroupStatsEntity> AdGroupStatsDbRepo;
        private readonly AdsDbRepository<AdStatsEntity> AdStatsDbRepo;

        public CreateAdFillRateRedisCommandHandler(
            IMediator mediator,
            AdsRedisContext adsRedis,
            AdsDbRepository<AdGroupStatsEntity> adGroupStatsDbRepo,
            AdsDbRepository<AdStatsEntity> adStatsDbRepo)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
            AdGroupStatsDbRepo = adGroupStatsDbRepo;
            AdStatsDbRepo = adStatsDbRepo;
        }

        public async Task<Unit> Handle(CreateAdFillRateRedisCommand request, CancellationToken cancellationToken)
        {
            var inventory = await AdGroupStatsDbRepo.SumAsync(
                filter: f => f.CaculatedAt == request.CaculatedAt.AddDays(-1) && f.AdGroupId == request.AdGroupId,
                selector: f => f.Request
            );
            if (inventory < 1)
            {
                inventory = await AdStatsDbRepo.SumAsync(
                    filter: f => f.CaculatedAt == request.CaculatedAt && f.Ad.AdUnitId == request.AdUnitId,
                    selector: f => f.Quota
                );
            }
            if (inventory < 1)
                return Unit.Value;

            var start = 0;
            var length = 100;
            var next = false;
            do
            {
                next = false;
                var getAdStatsList = await Mediator.Send(new GetAdStatsListQuery(start, length, request.CaculatedAt, request.AdUnitId, null), cancellationToken);
                if (getAdStatsList.IsSuccess)
                {
                    var adStats = getAdStatsList.Data;
                    foreach (var item in adStats)
                    {
                        var score = item.Quota;
                        var predicted = inventory * 1.2M;
                        var probability = (score / predicted) * 100;

                        var redisKey = CacheKey.UnitAdFillRate(request.AdUnitId);
                        var hashField = item.AdId;
                        var hashValue = probability.ToString("0.00");
                        await AdsRedis.HashSetAsync(redisKey, hashField, hashValue, When.Always, CommandFlags.FireAndForget);
                    }
                    next = adStats.Count() == length ? true : false;
                }
            }
            while (next);

            return Unit.Value;
        }
    }
}
