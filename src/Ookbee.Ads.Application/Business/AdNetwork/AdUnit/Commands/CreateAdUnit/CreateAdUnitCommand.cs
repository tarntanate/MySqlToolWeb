using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.AdUnit.Commands.CreateAdUnit
{
    public class CreateAdUnitCommand : CreateAdUnitRequest, IRequest<HttpResult<long>>
    {
        public CreateAdUnitCommand(CreateAdUnitRequest request)
        {
            AdGroupId = request.AdGroupId;
            AdNetwork = request.AdNetwork;
            AdNetworkUnitId = request.AdNetworkUnitId;
            AdNetworkUnitId_Android = request.AdNetworkUnitId_Android;
            SortSeq = request.SortSeq;
        }
    }
}
