using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.LoadTesting.HelloWorld.Queries.GetHelloWorldRedis
{
    public class GetHelloWorldRedisQueryHandler : IRequestHandler<GetHelloWorldRedisQuery, Response<string>>
    {
        private readonly IDatabase AdsRedis;

        public GetHelloWorldRedisQueryHandler(
            AdsRedisContext adsRedis)
        {
            AdsRedis = adsRedis.Database();
        }

        public async Task<Response<string>> Handle(GetHelloWorldRedisQuery request, CancellationToken cancellationToken)
        {
            var result = new Response<string>();
            var redisKey = RedisKeys.HelloWorld();
            var redisValues = await AdsRedis.StringGetAsync(redisKey);
            if (redisValues.HasValue())
                return result.OK(redisValues);
            return result.NotFound("Data not found.");
        }
    }
}
