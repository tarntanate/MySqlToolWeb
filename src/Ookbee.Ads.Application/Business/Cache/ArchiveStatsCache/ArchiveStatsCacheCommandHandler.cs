using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.Ad.Queries.GetAdList;
using Ookbee.Ads.Application.Business.AdNetwork.AdGroup.Queries.GetAdGroupList;
using Ookbee.Ads.Application.Business.AdNetwork.AdUnit.Queries.GetAdUnitList;
using Ookbee.Ads.Application.Business.Cache.AdStatsCache.Commands.ArchiveAdStatsCache;
using Ookbee.Ads.Application.Business.Cache.AdGroupStatsCache.Commands.ArchiveAdGroupStatsCache;
using Ookbee.Ads.Common;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Infrastructure.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ookbee.Ads.Application.Business.Cache.AdUnitStatsCache.Commands.ArchiveAdUnitStatsCache;

namespace Ookbee.Ads.Application.Business.Cache.ArchiveStatsCache
{
    public class ArchiveAdGroupStatsCommandHandler : IRequestHandler<ArchiveStatsCacheCommand>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }

        public ArchiveAdGroupStatsCommandHandler(
            IMapper mapper,
            IMediator mediator)
        {
            Mapper = mapper;
            Mediator = mediator;
        }

        public async Task<Unit> Handle(ArchiveStatsCacheCommand request, CancellationToken cancellationToken)
        {
            var next = true;

            do
            {
                var caculatedAt = MechineDateTime.Now.Date;
                await ArchiveAdGroupStats(caculatedAt, cancellationToken);
                var nowDateTime = MechineDateTime.Now;
                var nextDateTime = nowDateTime.RoundUp(TimeSpan.FromSeconds(5));
                var timeout = nextDateTime - nowDateTime;
                Thread.Sleep(timeout);
            }
            while (next);

            return Unit.Value;
        }

        public async Task ArchiveAdGroupStats(DateTime caculatedAt, CancellationToken cancellationToken)
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
                        await Mediator.Send(new ArchiveAdGroupStatsCacheCommand(adGroup.Id, caculatedAt), cancellationToken);
                        await ArchiveAdUnitStats(caculatedAt, adGroup.Id, cancellationToken);
                    }
                    start += length;
                }
                next = getAdGroupList.Data.Count() < length ? false : true;
            }
            while (next);
        }

        public async Task ArchiveAdUnitStats(DateTime caculatedAt, long adGroupId, CancellationToken cancellationToken)
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
                        await Mediator.Send(new ArchiveAdUnitStatsCacheCommand(adUnit.Id, caculatedAt), cancellationToken);
                        await ArchiveAdStats(caculatedAt, adUnit.Id, cancellationToken);
                    }
                    start += length;
                }
                next = getAdUnitList.Data.Count() < length ? false : true;
            }
            while (next);
        }

        public async Task ArchiveAdStats(DateTime caculatedAt, long adUnitId, CancellationToken cancellationToken)
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
                            await Mediator.Send(new ArchiveAdStatsCacheCommand(ad.Id, caculatedAt), cancellationToken);
                    }
                    start += length;
                }
                next = getAdList.Data.Count() < length ? false : true;
            }
            while (next);
        }
    }
}
