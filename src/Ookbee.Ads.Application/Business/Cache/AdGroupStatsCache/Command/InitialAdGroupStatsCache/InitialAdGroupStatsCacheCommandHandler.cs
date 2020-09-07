using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.AdGroup.Queries.GetAdGroupList;
using Ookbee.Ads.Application.Business.Analytics.AdGroupStat.Queries.GetAdGroupStatsById;
using Ookbee.Ads.Application.Business.Cache.AdGroupStatsCache.Commands.CreateAdGroupStatCache;
using Ookbee.Ads.Common;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.AdGroupStatsCache.Commands.InitialAdGroupStatsCache
{
    public class InitialAdGroupStatsCacheCommandHandler : IRequestHandler<InitialAdGroupStatsCacheCommand>
    {
        private IMediator Mediator { get; }
        private IDatabase AdsRedis { get; }

        public InitialAdGroupStatsCacheCommandHandler(
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(InitialAdGroupStatsCacheCommand request, CancellationToken cancellationToken)
        {
            var start = 0;
            var length = 100;
            var next = true;
            do
            {
                var getAdGroupList = await Mediator.Send(new GetAdGroupListQuery(start, length, null, null), cancellationToken);
                if (getAdGroupList.Ok)
                {
                    var caculatedAt = MechineDateTime.Now.Date;
                    foreach (var adGroup in getAdGroupList.Data)
                    {
                        var getAdGroupStatById = await Mediator.Send(new GetAdGroupStatsByIdQuery(adGroup.Id, caculatedAt), cancellationToken);
                        if (getAdGroupStatById.Ok)
                        {
                            var adGroupStat = getAdGroupStatById.Data;
                            var command = new CreateAdGroupStatsCacheCommand()
                            {
                                AdGroupId = adGroupStat.AdGroupId,
                                Platform = adGroupStat.Platform,
                                Stats = AdStats.Request,
                                Value = adGroupStat.Request,
                                CaculatedAt = caculatedAt,
                            };
                            await Mediator.Send(command, cancellationToken);
                        }
                    }
                    start += length;
                }
                next = getAdGroupList.Data.Count() < length ? false : true;
            }
            while (next);

            return Unit.Value;
        }
    }
}
