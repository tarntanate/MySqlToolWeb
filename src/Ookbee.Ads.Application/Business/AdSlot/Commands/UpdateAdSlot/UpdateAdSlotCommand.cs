using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdSlot.Commands.UpdateAdSlot
{
    public class UpdateAdSlotCommand : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public string PublisherId { get; set; }

        public string SlotTypeId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool EnabledFlag => true;

        public UpdateAdSlotCommand()
        {

        }

        public UpdateAdSlotCommand(string id, UpdateAdSlotCommand request)
        {
            Id = id;
            PublisherId = request.PublisherId;
            SlotTypeId = request.SlotTypeId;
            Name = request.Name;
            Description = request.Description;
        }
    }
}
