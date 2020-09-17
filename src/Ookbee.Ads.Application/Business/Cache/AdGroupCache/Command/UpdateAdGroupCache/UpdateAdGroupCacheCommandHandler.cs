using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Advertisement.AdGroup.Queries.GetAdGroupById;
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
        private IMediator Mediator { get; }
        private IDatabase AdsRedis { get; }

        public UpdateAdGroupCacheCommandHandler(
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
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
