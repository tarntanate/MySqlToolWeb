using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Advertisement.AdUnit.Commands.UpdateAdUnit
{
    public class UpdateAdUnitCommand : UpdateAdUnitRequest, IRequest<HttpResult<bool>>
    {
        public long Id { get; set; }

        public UpdateAdUnitCommand(long id, UpdateAdUnitRequest request)
        {
            Id = id;
            AdGroupId = request.AdGroupId;
            AdNetwork = request.AdNetwork;
            AdNetworkUnitId = request.AdNetworkUnitId;
            AdNetworkUnitId_Android = request.AdNetworkUnitId_Android;
            SortSeq = request.SortSeq;
        }
    }
}
