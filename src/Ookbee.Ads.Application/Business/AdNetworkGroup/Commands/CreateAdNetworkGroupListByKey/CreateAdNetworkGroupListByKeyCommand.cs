using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetworkGroup.Commands.CreateAdNetworkGroupListByKey
{
    public class CreateAdNetworkGroupListByKeyCommand : IRequest<HttpResult<bool>>
    {
        public CreateAdNetworkGroupListByKeyCommand()
        {

        }
    }
}
