using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroup.Commands.UpdateAdGroup
{
    public class UpdateAdGroupCommand : UpdateAdGroupRequest, IRequest<Response<bool>>
    {
        public long Id { get; private set; }

        public UpdateAdGroupCommand(long id, UpdateAdGroupRequest request)
        {
            Id = id;
            AdGroupTypeId = request.AdGroupTypeId;
            PublisherId = request.PublisherId;
            Name = request.Name;
            Description = request.Description;
            Placement = request.Placement;
            Enabled = request.Enabled;
        }
    }
}
