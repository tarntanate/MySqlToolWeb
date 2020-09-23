using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnit.Commands.UpdateAdUnit
{
    public class UpdateAdUnitCommand : IRequest<Response<bool>>
    {
        public long Id { get; private set; }
        public long AdGroupId { get; private set; }
        public string AdNetwork { get; private set; }
        public int? SortSeq { get; private set; }

        public UpdateAdUnitCommand(long id, UpdateAdUnitRequest request)
        {
            Id = id;
            AdGroupId = request.AdGroupId;
            AdNetwork = request.AdNetwork;
            SortSeq = request.SortSeq;
        }
    }
}
