using AutoMapper;
using MediatR;
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

namespace Ookbee.Ads.Application.Business.Cache.AdUnitCache.Commands.UpdateAdUnitCache
{
    public class UpdateAdUnitCacheCommandHandler : IRequestHandler<UpdateAdUnitCacheCommand, Unit>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private IDatabase AdsRedis { get; }

        public UpdateAdUnitCacheCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdsRedis = adsRedis.Database(0);
        }

        public async Task<Unit> Handle(UpdateAdUnitCacheCommand request, CancellationToken cancellationToken)
        {
            var start = 0;
            var length = 100;
            var next = true;
            var groups = new List<AdUnitCacheDto>();
            do
            {
                var getAdUnitList = await Mediator.Send(new GetAdUnitListQuery(start, length, request.AdGroupId));
                if (getAdUnitList.Ok)
                {
                    var units = getAdUnitList.Data;
                    foreach (var unit in units)
                    {
                        var group = Mapper.Map<AdUnitCacheDto>(unit);
                        groups.Add(group);
                    }
                }
                next = getAdUnitList.Data.Count() == length ? true : false;
                start += length;
            }
            while (next);

            var redisKey = CacheKey.UnitsByGroup(request.AdGroupId);
            if (groups.HasValue())
            {
                var redisValue = JsonHelper.Serialize(groups);
                await AdsRedis.StringSetAsync(redisKey, redisValue);
            }
            else
            {
                await AdsRedis.KeyDeleteAsync(redisKey);
            }


            return Unit.Value;
        }
    }
}
