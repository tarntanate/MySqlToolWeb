using MediatR;
using Ookbee.Ads.Application.Services.Advertisement.AdGroup.Queries.GetAdGroupIdList;
using Ookbee.Ads.Application.Services.CacheManager.AdGroupCache.Commands.CreateAdGroupIdCache;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.CacheManager.AdGroupCache.Commands.CreateAdGroupCache
{
    public class CreateAdGroupCacheCommandHandler : IRequestHandler<CreateAdGroupCacheCommand>
    {
        private readonly IMediator Mediator;
        private readonly IDatabase AdsRedis;

        public CreateAdGroupCacheCommandHandler(
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(CreateAdGroupCacheCommand request, CancellationToken cancellationToken)
        {
            var start = 0;
            var length = 100;
            var next = false;
            do
            {
                next = false;
                var response = await Mediator.Send(new GetAdGroupIdListQuery(start, length, null, null, true), cancellationToken);
                if (response.IsSuccess)
                {
                    var adGroupIds = response.Data;
                    next = adGroupIds.Count() == length ? true : false;
                    foreach(var adGroupId in adGroupIds)
                    {
                        await Mediator.Send(new CreateAdGroupIdCacheCommand(adGroupId), cancellationToken);
                    }
                }
            }
            while (next);
            
            return Unit.Value;
        }
    }
}
