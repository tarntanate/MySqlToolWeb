using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.LoadTesting.HelloWorld.Queries.GetHelloWorldRedis
{
    public class GetHelloWorldRedisQuery : IRequest<Response<string>>
    {
        public GetHelloWorldRedisQuery()
        {

        }
    }
}
