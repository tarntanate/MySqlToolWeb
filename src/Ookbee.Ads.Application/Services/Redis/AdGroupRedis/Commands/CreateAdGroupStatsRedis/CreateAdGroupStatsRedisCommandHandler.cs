using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Analytics.AdGroupStat.Commands.CreateAdGroupStats;
using Ookbee.Ads.Application.Services.Analytics.AdGroupStat.Queries.GetAdGroupStats;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Redis.AdGroupRedis.Commands.CreateAdGroupStatsRedis
{
    public class CreateAdGroupStatsRedisCommandHandler : IRequestHandler<CreateAdGroupStatsRedisCommand>
    {
        private readonly IMediator Mediator;
        private readonly IDatabase AdsRedis;

        public CreateAdGroupStatsRedisCommandHandler(
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(CreateAdGroupStatsRedisCommand request, CancellationToken cancellationToken)
        {
            var getAdGroupStat = await Mediator.Send(new GetAdGroupStatsQuery(request.AdGroupId, request.CaculatedAt), cancellationToken);
            if (getAdGroupStat.IsFail)
                await Mediator.Send(new CreateAdGroupStatsCommand(request.CaculatedAt, request.AdGroupId, 0), cancellationToken);

            var redisKey = CacheKey.GroupStats(request.AdGroupId);
            var hashField = AdStatsType.Request.ToString();
            var hashExists = await AdsRedis.HashExistsAsync(redisKey, hashField);
            if (!hashExists)
            {
                var hashFields = new List<HashEntry>();

                hashFields.Add(
                    new HashEntry(
                        AdStatsType.Quota.ToString(),
                        getAdGroupStat?.Data?.Request ?? 0L));

                var hashValue = getAdGroupStat?.Data?.Request ?? 0L;
                await AdsRedis.HashSetAsync(redisKey, hashFields.ToArray(), CommandFlags.FireAndForget);
            }

            return Unit.Value;
        }
    }
}
