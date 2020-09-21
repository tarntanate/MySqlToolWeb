using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Advertisement.AdNetwork.Commands.CreateAdNetwork
{
    public class CreateAdNetworkCommand : CreateAdNetworkRequest, IRequest<HttpResult<long>>
    {
        public CreateAdNetworkCommand(CreateAdNetworkRequest request)
        {
            AdUnitId = request.AdUnitId;
            AdNetworkUnitId = request.AdNetworkUnitId;
            Platform = request.Platform;
        }
    }
}
