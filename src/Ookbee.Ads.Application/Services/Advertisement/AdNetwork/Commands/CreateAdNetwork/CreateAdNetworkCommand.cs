using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Services.Advertisement.AdNetwork.Commands.CreateAdNetwork
{
    public class CreateAdNetworkCommand : IRequest<Response<long>>
    {
        public long AdUnitId { get; private set; }
        public string AdNetworkUnitId { get; private set; }
        public Platform Platform { get; private set; }

        public CreateAdNetworkCommand(CreateAdNetworkRequest request)
        {
            AdUnitId = request.AdUnitId;
            AdNetworkUnitId = request.AdNetworkUnitId;
            Platform = request.Platform;
        }
    }
}
