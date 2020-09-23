using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Business.Advertisement.AdGroup.Commands.UpdateAdGroup
{
    public class UpdateAdGroupCommand : UpdateAdGroupRequest, IRequest<Response<bool>>
    {
        public long Id { get; set; }

        public UpdateAdGroupCommand(long id, UpdateAdGroupRequest request)
        {
            Id = id;
            AdUnitTypeId = request.AdUnitTypeId;
            PublisherId = request.PublisherId;
            Name = request.Name;
            Description = request.Description;
        }
    }
}
