using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Business.AdGroup.Queries.GetAdGroupById;
using Ookbee.Ads.Application.Business.AdUnitCache.Commands.CreateAdUnitCache;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;


namespace Ookbee.Ads.Application.Business.AdGroupCache.Commands.CreateAdGroupCache
{
    public class CreateAdGroupCacheCommandHandler : IRequestHandler<CreateAdGroupCacheCommand, Unit>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private IDatabase AdsRedis { get; }

        public CreateAdGroupCacheCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdsRedis = adsRedis.Database(0);
        }

        public async Task<Unit> Handle(CreateAdGroupCacheCommand request, CancellationToken cancellationToken)
        {
            var getAdGroupById = await Mediator.Send(new GetAdGroupByIdQuery(request.AdGroupId), cancellationToken);
            if (getAdGroupById.Ok)
            {
                var adUnit = getAdGroupById.Data;
                var redisKey = CacheKey.Groups();
                var hashField = adUnit.Id;
                var hashValue = JsonHelper.Serialize(adUnit);
                await AdsRedis.HashSetAsync(redisKey, hashField, hashValue);
                await Mediator.Send(new CreateAdUnitCacheCommand(request.AdGroupId), cancellationToken);
            }

            return Unit.Value;
        }
    }
}
