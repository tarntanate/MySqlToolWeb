using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Advertisement.AdUnit.Commands.CreateAdUnit
{
    public class CreateAdUnitCommand : CreateAdUnitRequest, IRequest<HttpResult<long>>
    {
        public CreateAdUnitCommand(CreateAdUnitRequest request)
        {
            AdGroupId = request.AdGroupId;
            AdNetwork = request.AdNetwork;
            SortSeq = request.SortSeq;
        }
    }
}
