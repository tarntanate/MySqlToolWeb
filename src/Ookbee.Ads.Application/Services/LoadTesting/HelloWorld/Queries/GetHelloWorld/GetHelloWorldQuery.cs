using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.LoadTesting.HelloWorld.Queries.GetHelloWorld
{
    public class GetHelloWorldQuery : IRequest<Response<string>>
    {
        public GetHelloWorldQuery()
        {

        }
    }
}
