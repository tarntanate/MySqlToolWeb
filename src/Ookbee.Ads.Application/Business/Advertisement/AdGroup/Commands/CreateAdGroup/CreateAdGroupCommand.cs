using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Business.Advertisement.AdGroup.Commands.CreateAdGroup
{
    public class CreateAdGroupCommand : CreateAdGroupRequest, IRequest<Response<long>>
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
