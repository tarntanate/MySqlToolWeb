using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Redis.AdGroupRedis.Commands.GetAdGroupIdListByPublisherIdRedis
{
    public class GetAdGroupIdListByPublisherIdRedisQueryHandler : IRequestHandler<GetAdGroupIdListByPublisherIdRedisQuery, Response<string>>
    {
        private readonly IDatabase AdsRedis;

        public GetAdGroupIdListByPublisherIdRedisQueryHandler(
            AdsRedisContext adsRedis)
        {
            AdsRedis = adsRedis.Database();
        }

        public async Task<Response<string>> Handle(GetAdGroupIdListByPublisherIdRedisQuery request, CancellationToken cancellationToken)
        {
            var redisKey = CacheKey.GroupIdsPublisher();
            var hashField = $"{request.PublisherName}-{request.PublisherCountry}".ToUpper();
            var hashValue = await AdsRedis.HashGetAsync(redisKey, hashField);

            var result = new Response<string>();
            return (hashValue.HasValue())
                ? result.OK(hashValue)
                : result.NotFound();
        }
    }
}
