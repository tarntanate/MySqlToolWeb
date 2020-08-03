using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdGroupItem.Commands.CreateAdGroupItem
{
    public class CreateAdGroupItemCommand : CreateAdGroupItemRequest, IRequest<HttpResult<long>>
    {
        public CreateAdGroupItemCommand(CreateAdGroupItemRequest request)
        {
            AdGroupId = request.AdGroupId;
            AdUnitKey = request.AdUnitKey;
            Name = request.Name;
            Description = request.Description;
        }
    }
}
