using MediatR;
using Microsoft.Extensions.Hosting;
using Ookbee.Ads.Application.Services.Analytics.AdUnitStats.Commands.CreateAdUnitStats;
using Ookbee.Ads.Application.Services.Analytics.AdUnitStats.Commands.UpdateAdUnitStats;
using Ookbee.Ads.Application.Services.Analytics.AdUnitStats.Queries.GetAdUnitStats;
using Ookbee.Ads.Infrastructure;
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
            #region for tester
            if (!GlobalVar.HostingEnvironment.IsProduction())
            {
                var getYesterdayStats = await Mediator.Send(new GetAdUnitStatsQuery(request.CaculatedAt.AddDays(-1), request.AdUnitId), cancellationToken);
                if (getYesterdayStats.IsSuccess)
                {
                    var data = getYesterdayStats.Data;
                    await Mediator.Send(new UpdateAdUnitStatsCommand(request.CaculatedAt.AddDays(-1), getYesterdayStats.Data.Id, getYesterdayStats.Data.AdUnitId, 100, 100), cancellationToken);
                }
            }
            #endregion

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
