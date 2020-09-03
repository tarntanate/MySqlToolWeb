using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.AdGroup.Queries.GetAdGroupById;
using Ookbee.Ads.Application.Business.AdNetwork.AdUnit.Queries.GetAdUnitList;
using Ookbee.Ads.Application.Business.Cache.AdUnitCache.Commands.UpdateAdUnitCache;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.AdGroupCache.Commands.UpdateAdGroupCache
{
    public class UpdateAdGroupCacheCommandHandler : IRequestHandler<UpdateAdGroupCacheCommand>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private IDatabase AdsRedis { get; }

        public UpdateAdGroupCacheCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(UpdateAdGroupCacheCommand request, CancellationToken cancellationToken)
        {
            var getAdGroupById = await Mediator.Send(new GetAdGroupByIdQuery(request.AdGroupId), cancellationToken);
            if (getAdGroupById.Ok)
            {
                var adUnit = getAdGroupById.Data;
                var redisKey = CacheKey.Groups();
                var hashField = adUnit.Id;
                var hashValue = JsonHelper.Serialize(adUnit);
                await AdsRedis.HashSetAsync(redisKey, hashField, hashValue);
            }

            return Unit.Value;
        }
    }
}
