using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Analytics.AdStats.Commands.CreateAdStats;
using Ookbee.Ads.Application.Services.Analytics.AdStats.Queries.GetAdQuota;
using Ookbee.Ads.Application.Services.Analytics.AdStats.Queries.GetAdStats;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Collections.Generic;
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
            var getAdStats = await Mediator.Send(new GetAdStatsQuery(request.AdId, request.CaculatedAt), cancellationToken);
            if (getAdStats.IsFail)
            {
                var getAdQuotaById = await Mediator.Send(new GetAdQuotaQuery(request.AdId, request.CaculatedAt), cancellationToken);
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
            var Impression = getAdStats?.Data?.Impression ?? 0L;
            if (quota > Impression)
            {
                var redisKey = CacheKey.AdStats(request.AdId);
                var keyExists = await AdsRedis.KeyExistsAsync(redisKey);
                if (!keyExists)
                {
                    var hashFields = new List<HashEntry>();

                    hashFields.Add(
                        new HashEntry(
                            AdStatsType.Quota.ToString(),
                            getAdStats?.Data?.Quota ?? 0L));

                    hashFields.Add(
                        new HashEntry(
                            AdStatsType.Click.ToString(),
                            getAdStats?.Data?.Click ?? 0L));

                    hashFields.Add(
                        new HashEntry(
                            AdStatsType.Impression.ToString(),
                            getAdStats?.Data?.Impression ?? 0L));

                    await AdsRedis.HashSetAsync(redisKey, hashFields.ToArray(), CommandFlags.FireAndForget);
                }
            }

            return Unit.Value;
        }
    }
}
