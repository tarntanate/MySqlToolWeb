using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Analytics.AdStats.Commands.CreateAdStats;
using Ookbee.Ads.Application.Services.Analytics.AdStats.Queries.GetAdStats;
using Ookbee.Ads.Application.Services.Analytics.AdStats.Queries.GetAvailableQuota;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Redis.AdRedis.Commands.CreateAdStatsRedis
{
    public class CreateAdStatsRedisCommandHandler : IRequestHandler<CreateAdStatsRedisCommand>
    {
        private readonly IMediator Mediator;
        private readonly IDatabase AdsRedis;

        public CreateAdStatsRedisCommandHandler(
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(CreateAdStatsRedisCommand request, CancellationToken cancellationToken)
        {
            var getAdStats = await Mediator.Send(new GetAdStatsQuery(request.CaculatedAt, request.AdId), cancellationToken);
            if (getAdStats.IsFail)
            {
                var getAdQuotaById = await Mediator.Send(new GetAvailableQuotaQuery(request.CaculatedAt, request.AdId), cancellationToken);
                if (getAdQuotaById.IsSuccess)
                {
                    var adQuota = getAdQuotaById?.Data ?? 0L;
                    if (adQuota > 0)
                    {
                        await Mediator.Send(new CreateAdStatsCommand(request.AdId, request.CaculatedAt, adQuota), cancellationToken);
                    }
                }
            }

            var quota = getAdStats?.Data?.Quota ?? 0L;
            var impression = getAdStats?.Data?.Impression ?? 0L;
            if (quota > impression)
            {
                var redisKey = CacheKey.AdStats(request.AdId);
                var redisValue = await AdsRedis.HashGetAllAsync(redisKey);
                var hashFields = new List<HashEntry>();

                var quotaCache = (long)(redisValue?.FirstOrDefault(x => x.Name == AdStatsType.Quota.ToString()).Value ?? 0L);
                var quotaDb = getAdStats?.Data?.Quota ?? 0L;
                if (quotaDb != quotaCache)
                    hashFields.Add(new HashEntry(AdStatsType.Quota.ToString(), quotaDb));

                var clickCache = (long)(redisValue?.FirstOrDefault(x => x.Name == AdStatsType.Click.ToString()).Value ?? 0L);
                var clickDb = getAdStats?.Data?.Click ?? 0L;
                if (clickDb > clickCache)
                    hashFields.Add(new HashEntry(AdStatsType.Click.ToString(), clickDb));

                var impressionCache = (long)(redisValue?.FirstOrDefault(x => x.Name == AdStatsType.Impression.ToString()).Value ?? 0L);
                var impressionDb = getAdStats?.Data?.Impression ?? 0L;
                if (impressionDb > impressionCache)
                    hashFields.Add(new HashEntry(AdStatsType.Impression.ToString(), impressionDb));

                await AdsRedis.HashSetAsync(redisKey, hashFields.ToArray(), CommandFlags.FireAndForget);
            }

            return Unit.Value;
        }
    }
}
