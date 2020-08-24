using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.Commands.CreateCache
{
    public class CreateCacheCommand : IRequest<HttpResult<bool>>
    {
        public CreateCacheCommand()
        {

        }
    }
}
