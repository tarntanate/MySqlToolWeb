using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdUnit.Commands.CreateAdUnit
{
    public class CreateAdUnitCommand : CreateAdUnitRequest, IRequest<HttpResult<long>>
    {
        public CreateAdUnitCommand(CreateAdUnitRequest request)
        {
            AdUnitTypeId = request.AdUnitTypeId;
            PublisherId = request.PublisherId;
            Name = request.Name;
            Description = request.Description;
            AdNetworks = request.AdNetworks;
        }
    }
}
