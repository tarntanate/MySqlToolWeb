using MediatR;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.LoadTesting.HelloWorld.Queries.GetHelloWorld
{
    public class GetHelloWorldQueryHandler : IRequestHandler<GetHelloWorldQuery, Response<string>>
    {
        private readonly IDatabase AdsRedis;

        public GetHelloWorldQueryHandler(
            AdsRedisContext adsRedis)
        {
            AdsRedis = adsRedis.Database();
        }

        public async Task<Response<string>> Handle(GetHelloWorldQuery request, CancellationToken cancellationToken)
        {
            var result = new Response<string>();
            var redisValues = string.Empty;
            await Task.Run(() => { redisValues = "{\"test\": \"HelloWorld\"}"; });

            if (redisValues.HasValue())
                return result.OK(redisValues);
            return result.NotFound();
        }
    }
}
