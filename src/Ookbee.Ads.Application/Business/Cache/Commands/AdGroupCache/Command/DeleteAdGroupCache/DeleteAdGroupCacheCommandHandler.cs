using MediatR;
using Ookbee.Ads.Application.Business.AdUnit.Queries.GetAdUnitList;
using Ookbee.Ads.Application.Business.Cache.AdUnitCache.Commands.DeleteAdUnitCache;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.AdGroupCache.Commands.DeleteAdGroupCache
{
    public class DeleteAdGroupCacheCommandHandler : IRequestHandler<DeleteAdGroupCacheCommand, Unit>
    {
        private IMediator Mediator { get; }
        private IDatabase AdsRedis { get; }

        public DeleteAdGroupCacheCommandHandler(
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database(0);
        }

        public async Task<Unit> Handle(DeleteAdGroupCacheCommand request, CancellationToken cancellationToken)
        {
            var redisKey = CacheKey.Groups();
            var hashField = request.AdGroupId;
            await DeleteAdUnitCache(request.AdGroupId, cancellationToken);
            await AdsRedis.HashDeleteAsync(redisKey, hashField);

            return Unit.Value;
        }

        public async Task DeleteAdUnitCache(long adGroupId, CancellationToken cancellationToken)
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
                        await Mediator.Send(new DeleteAdUnitCacheCommand(adUnit.AdGroup.Id, adUnit.AdNetwork), cancellationToken);
                    }
                    start += length;
                }
                next = getAdUnitList.Data.Count() < length ? false : true;
            }
            while (next);
        }
    }
}
