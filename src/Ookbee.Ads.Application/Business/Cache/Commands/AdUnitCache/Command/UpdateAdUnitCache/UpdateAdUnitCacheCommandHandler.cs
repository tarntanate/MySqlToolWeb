using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Business.AdUnit.Queries.GetAdUnitById;
using Ookbee.Ads.Application.Business.Cache.AdUnitCache.Commands.CreateAdUnitCache;
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
            var getAdUnitById = await Mediator.Send(new GetAdUnitByIdQuery(request.AdUnitId), cancellationToken);
            if (getAdUnitById.Ok)
            {
                var adUnit = Mapper.Map<AdUnitCacheDto>(getAdUnitById.Data);
                var redisKey = CacheKey.UnitsByGroup(getAdUnitById.Data.AdGroup.Id);
                var redisValue = await AdsRedis.StringGetAsync(redisKey);
                if (redisValue.HasValue())
                {
                    var adUnits = JsonHelper.Deserialize<List<AdUnitCacheDto>>(redisValue);
                    var index = adUnits.FindIndex(x => x.Id == request.AdUnitId);
                    if (index > -1)
                    {
                        adUnits[index] = adUnit;
                    }
                    var json = JsonHelper.Serialize(adUnits);
                    await AdsRedis.StringSetAsync(redisKey, json);
                }
            }

            return Unit.Value;
        }
    }
}
