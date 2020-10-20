using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroup.Commands.UpdateAdGroup
{
    public class UpdateAdGroupCommand : IRequest<Response<bool>>
    {
        public long Id { get; private set; }
        public long AdUnitTypeId { get; private set; }
        public long PublisherId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool Enabled { get; private set; }

        public UpdateAdGroupCommand(long id, UpdateAdGroupRequest request)
        {
            Id = id;
            AdUnitTypeId = request.AdUnitTypeId;
            PublisherId = request.PublisherId;
            Name = request.Name;
            Description = request.Description;
            Enabled = request.Enabled;
        }
    }
}
