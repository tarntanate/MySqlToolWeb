using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Advertisement.AdNetwork.Commands.UpdateAdNetwork
{
    public class UpdateAdNetworkCommand : UpdateAdNetworkRequest, IRequest<HttpResult<bool>>
    {
        public long Id { get; set; }

        public UpdateAdNetworkCommand(long id, UpdateAdNetworkRequest request)
        {
            Id = id;
            AdUnitId = request.AdUnitId;
            AdNetworkUnitId = request.AdNetworkUnitId;
            Platform = request.Platform;
        }
    }
}
