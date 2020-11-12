using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.AdNetwork.Commands.UpdateAdNetwork
{
    public class UpdateAdNetworkCommand : UpdateAdNetworkRequest, IRequest<Response<bool>>
    {
        public long Id { get; private set; }

        public UpdateAdNetworkCommand(long id, UpdateAdNetworkRequest request)
        {
            Id = id;
            AdUnitId = request.AdUnitId;
            AdNetworkUnitId = request.AdNetworkUnitId;
            Platform = request.Platform;
        }
    }
}
