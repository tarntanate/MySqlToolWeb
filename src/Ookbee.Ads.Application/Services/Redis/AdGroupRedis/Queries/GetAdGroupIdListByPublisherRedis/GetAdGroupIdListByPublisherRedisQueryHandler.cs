using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Redis.AdGroupRedis.Commands.GetAdGroupIdListByPublisherRedis
{
    public class GetAdGroupIdListByPublisherRedisQueryHandler : IRequestHandler<GetAdGroupIdListByPublisherRedisQuery, Response<Dictionary<string, List<long>>>>
    {
        private readonly IDatabase AdsRedis;

        public GetAdGroupIdListByPublisherRedisQueryHandler(
            AdsRedisContext adsRedis)
        {
            AdsRedis = adsRedis.Database();
        }

        public async Task<Response<Dictionary<string, List<long>>>> Handle(GetAdGroupIdListByPublisherRedisQuery request, CancellationToken cancellationToken)
        {
            var redisKey = CacheKey.GroupIdsPublisher();
            var hashValue = await AdsRedis.HashGetAllAsync(redisKey);

            var result = new Response<Dictionary<string, List<long>>>();
            return (hashValue.HasValue())
                ? result.OK(hashValue.ToDictionary(v => v.Name.ToString(), v => JsonHelper.Deserialize<List<long>>(v.Value)))
                : result.NotFound();
        }
    }
}
