using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Services.Advertisement.AdNetwork.Commands.UpdateAdNetwork
{
    public class UpdateAdNetworkCommand : IRequest<Response<bool>>
    {
        public long Id { get; private set; }
        public long AdUnitId { get; private set; }
        public string AdNetworkUnitId { get; private set; }
        public Platform Platform { get; private set; }

        public UpdateAdNetworkCommand(long id, UpdateAdNetworkRequest request)
        {
            Id = id;
            AdUnitId = request.AdUnitId;
            AdNetworkUnitId = request.AdNetworkUnitId;
            Platform = request.Platform;
        }
    }
}
