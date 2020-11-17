using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Redis.AdGroupRedis.Commands.GetAdUnitIdListRedis
{
    public class GetAdUnitIdListRedisQueryHandler : IRequestHandler<GetAdUnitIdListRedisQuery, Response<IEnumerable<long>>>
    {
        private readonly IDatabase AdsRedis;

        public GetAdUnitIdListRedisQueryHandler(
            AdsRedisContext adsRedis)
        {
            AdsRedis = adsRedis.Database();
        }

        public async Task<Response<IEnumerable<long>>> Handle(GetAdUnitIdListRedisQuery request, CancellationToken cancellationToken)
        {
            var items = new List<long>();
            var redisKey = RedisKeys.GroupUnitIds(request.AdGroupId);
            var redisValues = await AdsRedis.SetMembersAsync(redisKey);
            if (redisValues.HasValue())
                items = redisValues.Select(redisValue => (long)redisValue).ToList();

            var result = new Response<IEnumerable<long>>();
            return (items.HasValue())
                ? result.OK(items)
                : result.NotFound();
        }
    }
}
