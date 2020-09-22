using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Business.Advertisement.AdUnit.Commands.CreateAdUnit
{
    public class CreateAdUnitCommand : CreateAdUnitRequest, IRequest<Response<long>>
    {
        public CreateAdUnitCommand(CreateAdUnitRequest request)
        {
            AdGroupId = request.AdGroupId;
            AdNetwork = request.AdNetwork;
            SortSeq = request.SortSeq;
        }
    }
}
