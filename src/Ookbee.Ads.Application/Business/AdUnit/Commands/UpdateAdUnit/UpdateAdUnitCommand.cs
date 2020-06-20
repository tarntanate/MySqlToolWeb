using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdUnit.Commands.UpdateAdUnit
{
    public class UpdateAdUnitCommand : UpdateAdUnitRequest, IRequest<HttpResult<bool>>
    {
        public long Id { get; set; }

        public UpdateAdUnitCommand(long id, UpdateAdUnitRequest request)
        {
            Id = id;
            AdUnitTypeId = request.AdUnitTypeId;
            PublisherId = request.PublisherId;
            Name = request.Name;
            Description = request.Description;
        }
    }
}
