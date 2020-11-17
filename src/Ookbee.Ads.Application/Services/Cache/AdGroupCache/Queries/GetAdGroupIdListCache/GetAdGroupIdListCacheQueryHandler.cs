using MediatR;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Cache.AdGroupCache.Queries.GetAdGroupIdListCache
{
    public class GetAdGroupIdListCacheHandler : IRequestHandler<GetAdGroupIdListCacheQuery, Response<IEnumerable<long>>>
    {
        private readonly IMediator Mediator;
        private readonly IDatabase AdsRedis;

        public GetAdGroupIdListCacheHandler(
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
        }

        public async Task<Response<IEnumerable<long>>> Handle(GetAdGroupIdListCacheQuery request, CancellationToken cancellationToken)
        {
            var redisKey = CacheKey.GroupIdList();
            var redisValues = await AdsRedis.SetMembersAsync(redisKey);
            var items = redisValues.Select(redisValue => (long)redisValue).ToList();

            var result = new Response<IEnumerable<long>>();
            return items.HasValue()
                ? result.OK(items)
                : result.NotFound();
        }
    }
}
