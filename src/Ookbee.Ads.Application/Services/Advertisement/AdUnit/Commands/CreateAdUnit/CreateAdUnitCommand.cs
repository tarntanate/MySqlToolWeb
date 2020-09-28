using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnit.Commands.CreateAdUnit
{
    public class CreateAdUnitCommand : IRequest<Response<long>>
    {
        public long AdGroupId { get; private set; }
        public string AdNetwork { get; private set; }
        public int? SortSeq { get; private set; }

        public CreateAdUnitCommand(CreateAdUnitRequest request)
        {
            AdGroupId = request.AdGroupId;
            AdNetwork = request.AdNetwork;
            SortSeq = request.SortSeq;
        }
    }
}
