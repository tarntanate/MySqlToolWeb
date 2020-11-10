using MediatR;
using Ookbee.Ads.Application.Services.Analytics.AdUnitStats.Commands.CreateAdUnitStats;
using Ookbee.Ads.Application.Services.Analytics.AdUnitStats.Queries.GetAdUnitStats;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
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
            var getYesterdayStats = await Mediator.Send(new GetAdUnitStatsQuery(request.CaculatedAt.AddDays(-1), request.AdUnitId), cancellationToken);
            var getTodayStats = await Mediator.Send(new GetAdUnitStatsQuery(request.CaculatedAt, request.AdUnitId), cancellationToken);
            if (getTodayStats.IsFail)
            {
                var data = getTodayStats.Data;
                await Mediator.Send(new CreateAdUnitStatsCommand(request.CaculatedAt, request.AdUnitId, 0, 0), cancellationToken);
            }

            return Unit.Value;
        }
    }
}
