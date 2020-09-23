using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Business.Advertisement.AdUnit.Commands.UpdateAdUnit
{
    public class UpdateAdUnitCommand : UpdateAdUnitRequest, IRequest<Response<bool>>
    {
        public long Id { get; set; }

        public UpdateAdUnitCommand(long id, UpdateAdUnitRequest request)
        {
            Id = id;
            AdGroupId = request.AdGroupId;
            AdNetwork = request.AdNetwork;
            SortSeq = request.SortSeq;
        }
    }
}
