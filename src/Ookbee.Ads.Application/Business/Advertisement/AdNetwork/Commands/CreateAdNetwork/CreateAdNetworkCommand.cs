using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Business.Advertisement.AdNetwork.Commands.CreateAdNetwork
{
    public class CreateAdNetworkCommand : CreateAdNetworkRequest, IRequest<Response<long>>
    {
        public CreateAdNetworkCommand(CreateAdNetworkRequest request)
        {
            AdUnitId = request.AdUnitId;
            AdNetworkUnitId = request.AdNetworkUnitId;
            Platform = request.Platform;
        }
    }
}
