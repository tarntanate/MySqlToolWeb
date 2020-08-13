using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdGroup.Commands.UpdateAdGroup
{
    public class UpdateAdGroupCommand : UpdateAdGroupRequest, IRequest<HttpResult<bool>>
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
