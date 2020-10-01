using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Analytics.AdUnitStats.Commands.CreateAdUnitStats;
using Ookbee.Ads.Application.Services.Analytics.AdUnitStats.Queries.GetAdUnitStats;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Redis.AdUnitRedis.Commands.CreateAdUnitStatsRedis
{
    public class CreateAdUnitStatsRedisCommandHandler : IRequestHandler<CreateAdUnitStatsRedisCommand>
    {
        private readonly IMediator Mediator;
        private readonly IDatabase AdsRedis;

        public CreateAdUnitStatsRedisCommandHandler(
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(CreateAdUnitStatsRedisCommand request, CancellationToken cancellationToken)
        {
            var getAdUnitStats = await Mediator.Send(new GetAdUnitStatsQuery(request.AdUnitId, request.CaculatedAt), cancellationToken);
            if (getAdUnitStats.IsFail)
            {
                var data = getAdUnitStats.Data;
                await Mediator.Send(new CreateAdUnitStatsCommand(request.CaculatedAt, request.AdUnitId, 0, 0), cancellationToken);
            }

            var redisKey = CacheKey.UnitStats(request.AdUnitId);
            var keyExists = await AdsRedis.KeyExistsAsync(redisKey);
            if (!keyExists)
            {
                var hashFields = new List<HashEntry>();

                hashFields.Add(
                    new HashEntry(
                        AdStatsType.Request.ToString(), 
                        getAdUnitStats?.Data?.Request ?? 0L));

                hashFields.Add(
                    new HashEntry(
                        AdStatsType.Fill.ToString(), 
                        getAdUnitStats?.Data?.Fill ?? 0L));

                await AdsRedis.HashSetAsync(redisKey, hashFields.ToArray(), CommandFlags.FireAndForget);
            }

            return Unit.Value;
        }
    }
}
