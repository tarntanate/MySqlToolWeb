using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Analytics.AdStat.Queries.GetAdStatsList;
using Ookbee.Ads.Common;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Infrastructure.Models;
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
        private readonly AdsDbRepository<AdEntity> AdDbRepo;
        private readonly AdsDbRepository<AdStatsEntity> AdStatsDbRepo;
        private readonly AdsDbRepository<AdUnitStatsEntity> AdUnitStatsDbRepo;

        public CreateAdFillRateRedisCommandHandler(
            IMediator mediator,
            AdsRedisContext adsRedis,
            AdsDbRepository<AdEntity> adDbRepo,
            AdsDbRepository<AdStatsEntity> adStatsDbRepo,
            AdsDbRepository<AdUnitStatsEntity> adUnitStatsDbRepo)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
            AdDbRepo = adDbRepo;
            AdStatsDbRepo = adStatsDbRepo;
            AdUnitStatsDbRepo = adUnitStatsDbRepo;
        }

        public async Task<Unit> Handle(CreateAdFillRateRedisCommand request, CancellationToken cancellationToken)
        {
            var totalRequest = await AdUnitStatsDbRepo.SumAsync(
                filter: f => f.CaculatedAt == request.CaculatedAt.AddDays(-1) && f.AdUnitId == request.AdUnitId,
                selector: f => f.Request
            );

            var totalQuota = await AdStatsDbRepo.SumAsync(
                filter: f => f.CaculatedAt == request.CaculatedAt.AddDays(-1) && f.Ad.AdUnitId == request.AdUnitId,
                selector: f => f.Quota
            );

            if (totalRequest < totalQuota)
                totalRequest = totalQuota;

            var start = 0;
            var length = 100;
            var next = false;
            do
            {
                next = false;
                var getAdStatsList = await Mediator.Send(new GetAdStatsListQuery(start, length, request.CaculatedAt, request.AdUnitId, null), cancellationToken);
                if (getAdStatsList.IsSuccess)
                {
                    foreach (var adStats in getAdStatsList.Data)
                    {
                        var isExistsAd = await AdDbRepo.AnyAsync(
                            filter: f =>
                            f.Id == adStats.AdId &&
                            f.StartAt <= MechineDateTime.Now &&
                            f.EndAt >= MechineDateTime.Now &&
                            f.Status == AdStatusType.Publish
                        );
                        if (isExistsAd)
                        {
                            var probability = (adStats.Quota / totalRequest) * 100;
                            var redisKey = RedisKeys.UnitAdFillRate(request.AdUnitId);
                            var hashField = adStats.AdId;
                            var hashValue = probability.ToString("0.00");
                            await AdsRedis.HashSetAsync(redisKey, hashField, hashValue, When.Always, CommandFlags.FireAndForget);
                        }
                    }
                    next = getAdStatsList.Data.Count() == length ? true : false;
                    start += length;
                }
            }
            while (next);

            return Unit.Value;
        }
    }
}
