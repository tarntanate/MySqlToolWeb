using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.Commands.CreateAdByUnitId;
using Ookbee.Ads.Application.Business.AdUnit.Queries.GetAdUnitList;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdNetwork.Commands.CreateUnitListByGroupId
{
    public class CreateUnitListByGroupIdCommandHandler : IRequestHandler<CreateUnitListByGroupIdCommand, Unit>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private IDatabase AdsRedis { get; }

        public CreateUnitListByGroupIdCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdsRedis = adsRedis.Database(0);
        }

        public async Task<Unit> Handle(CreateUnitListByGroupIdCommand request, CancellationToken cancellationToken)
        {
            var start = 0;
            var length = 100;
            var next = true;
            var groups = new List<AdNetworkGroupUnitDto>();
            do
            {
                var getAdUnitList = await Mediator.Send(new GetAdUnitListQuery(start, length, request.AdGroupId));
                if (getAdUnitList.Ok)
                {
                    var units = getAdUnitList.Data;
                    foreach (var unit in units)
                    {
                        await Mediator.Send(new CreateAdByUnitIdCommand(unit.Id));
                        var group = Mapper.Map<AdNetworkGroupUnitDto>(unit);
                        groups.Add(group);
                    }
                }
                next = getAdUnitList.Data.Count() == length ? true : false;
                start += length;
            }
            while (next);

            if (groups.HasValue())
            {
                var redisKey = CacheKey.UnitsByGroup(request.AdGroupId);
                var redisValue = JsonHelper.Serialize(groups);
                await AdsRedis.StringSetAsync(redisKey, redisValue);
            }

            return Unit.Value;
        }
    }
}
