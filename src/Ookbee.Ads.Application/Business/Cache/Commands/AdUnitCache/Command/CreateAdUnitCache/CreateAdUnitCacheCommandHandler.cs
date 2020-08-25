using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Ad.Queries.GetAdList;
using Ookbee.Ads.Application.Business.Cache.AdAssetCache.Commands.CreateAdAssetCache;
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

namespace Ookbee.Ads.Application.Business.Cache.AdUnitCache.Commands.CreateAdUnitCache
{
    public class CreateAdUnitCacheCommandHandler : IRequestHandler<CreateAdUnitCacheCommand, Unit>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private IDatabase AdsRedis { get; }

        public CreateAdUnitCacheCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdsRedis = adsRedis.Database(0);
        }

        public async Task<Unit> Handle(CreateAdUnitCacheCommand request, CancellationToken cancellationToken)
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
                        await CreateAdAssetCache(unit.Id, cancellationToken);
                        var group = Mapper.Map<AdUnitCacheDto>(unit);
                        groups.Add(group);
                    }
                }
                next = getAdUnitList.Data.Count() == length ? true : false;
                start += length;
            }
            while (next);

            var redisKey = CacheKey.UnitsByGroup(request.AdGroupId);
            var redisValue = groups.HasValue() ? JsonHelper.Serialize(groups) : string.Empty;
            await AdsRedis.StringSetAsync(redisKey, redisValue);

            return Unit.Value;
        }

        public async Task CreateAdAssetCache(long adUnitId, CancellationToken cancellationToken)
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
                        await Mediator.Send(new CreateAdAssetCacheCommand(ad.Id), cancellationToken);
                    }
                    start += length;
                }
                next = getAdList.Data.Count() < length ? false : true;
            }
            while (next);
        }
    }
}
