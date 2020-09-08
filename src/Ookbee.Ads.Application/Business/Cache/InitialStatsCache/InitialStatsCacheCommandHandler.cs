using AutoMapper;
using MediatR;
using Ookbee.Ads.Common;
using Ookbee.Ads.Application.Business.AdNetwork.Ad.Queries.GetAdList;
using Ookbee.Ads.Application.Business.AdNetwork.AdGroup.Queries.GetAdGroupList;
using Ookbee.Ads.Application.Business.Analytics.AdGroupStat.Commands.InitialAdGroupStats;
using Ookbee.Ads.Application.Business.AdNetwork.AdUnit.Queries.GetAdUnitList;
using Ookbee.Ads.Application.Business.Analytics.AdUnitStats.Commands.InitialAdUnitStats;
using Ookbee.Ads.Application.Business.Analytics.AdAssetStats.Commands.InitialAssetAdStats;
using Ookbee.Ads.Infrastructure.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.Commands.InitialStatsCache
{
    public class InitialStatsCacheCommandHandler : IRequestHandler<InitialStatsCacheCommand>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private DateTime CaculatedAt { get; }

        public InitialStatsCacheCommandHandler(
            IMapper mapper,
            IMediator mediator)
        {
            Mapper = mapper;
            Mediator = mediator;
            CaculatedAt = MechineDateTime.Now.Date;
        }

        public async Task<Unit> Handle(InitialStatsCacheCommand request, CancellationToken cancellationToken)
        {
            await InitialAdGroupStats(request, cancellationToken);

            return Unit.Value;
        }

        public async Task InitialAdGroupStats(InitialStatsCacheCommand request, CancellationToken cancellationToken)
        {
            var start = 0;
            var length = 100;
            var next = true;
            do
            {
                var getAdGroupList = await Mediator.Send(new GetAdGroupListQuery(start, length, null, null), cancellationToken);
                if (getAdGroupList.Ok)
                {
                    foreach (var adGroup in getAdGroupList.Data)
                    {
                        await Mediator.Send(new InitialAdGroupStatsCommand(adGroup.Id, CaculatedAt), cancellationToken);
                        await InitialAdUnitStats(adGroup.Id, cancellationToken);
                    }
                    start += length;
                }
                next = getAdGroupList.Data.Count() < length ? false : true;
            }
            while (next);
        }

        public async Task InitialAdUnitStats(long adGroupId, CancellationToken cancellationToken)
        {
            var start = 0;
            var length = 100;
            var next = true;
            do
            {
                var getAdUnitList = await Mediator.Send(new GetAdUnitListQuery(start, length, adGroupId), cancellationToken);
                if (getAdUnitList.Ok)
                {
                    foreach (var adUnit in getAdUnitList.Data)
                    {
                        await Mediator.Send(new InitialAdUnitStatsCommand(adUnit.Id, CaculatedAt), cancellationToken);
                        await InitialAdStats(adUnit.Id, cancellationToken);
                    }
                    start += length;
                }
                next = getAdUnitList.Data.Count() < length ? false : true;
            }
            while (next);
        }

        public async Task InitialAdStats(long adUnitId, CancellationToken cancellationToken)
        {
            var start = 0;
            var length = 100;
            var next = true;
            do
            {
                var getAdList = await Mediator.Send(new GetAdListQuery(start, length, adUnitId, null), cancellationToken);
                if (getAdList.Ok)
                {
                    foreach (var ad in getAdList.Data)
                    {
                        if (ad.Status == AdStatus.Publish || ad.Status == AdStatus.Preview)
                            await Mediator.Send(new InitialAdAssetStatsCommand(ad.Id, CaculatedAt), cancellationToken);
                    }
                    start += length;
                }
                next = getAdList.Data.Count() < length ? false : true;
            }
            while (next);
        }
    }
}
