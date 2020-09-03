using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.AdGroup.Queries.GetAdGroupList;
using Ookbee.Ads.Application.Business.Analytics.AdGroupStat.Queries.GetAdGroupStatById;
using Ookbee.Ads.Application.Business.Cache.AdGroupStatsCache.Commands.CreateAdGroupStatCache;
using Ookbee.Ads.Common;
using Ookbee.Ads.Infrastructure.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.Commands.InitialStatsCache
{
    public class InitialStatsCacheCommandHandler : IRequestHandler<InitialStatsCacheCommand, Unit>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }

        public InitialStatsCacheCommandHandler(
            IMapper mapper,
            IMediator mediator)
        {
            Mapper = mapper;
            Mediator = mediator;
        }

        public async Task<Unit> Handle(InitialStatsCacheCommand request, CancellationToken cancellationToken)
        {
            try
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
                            var getAdGroupStatById = await Mediator.Send(new GetAdGroupStatByIdQuery(adGroup.Id, caculatedAt), cancellationToken);
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
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Unit.Value;
        }
    }
}
