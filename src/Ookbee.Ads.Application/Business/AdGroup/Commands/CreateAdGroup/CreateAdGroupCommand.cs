using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdGroup.Commands.CreateAdGroup
{
    public class CreateAdGroupCommand : CreateAdGroupRequest, IRequest<HttpResult<long>>
    {
        public CreateAdGroupCommand(CreateAdGroupRequest request)
        {
            AdUnitTypeId = request.AdUnitTypeId;
            PublisherId = request.PublisherId;
            Name = request.Name;
            Description = request.Description;
        }
    }
}
