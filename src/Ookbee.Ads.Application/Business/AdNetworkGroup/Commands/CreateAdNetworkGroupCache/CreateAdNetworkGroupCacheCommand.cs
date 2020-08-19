using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetworkGroup.Commands.CreateAdNetworkGroupCache
{
    public class CreateAdNetworkGroupCacheCommand : IRequest<HttpResult<bool>>
    {
        public CreateAdNetworkGroupCacheCommand()
        {

        }
    }
}
