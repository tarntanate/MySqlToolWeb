using MediatR;
using MongoDB.Bson;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdSlot.Commands.CreateAdSlot
{
    public class CreateAdSlotCommand : IRequest<HttpResult<string>>
    {
        public string Id => ObjectId.GenerateNewId().ToString();

        public string PublisherId { get; set; }

        public string SlotTypeId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool EnabledFlag => true;

        public CreateAdSlotCommand()
        {

        }

        public CreateAdSlotCommand(string publisherId, CreateAdSlotCommand request)
        {
            PublisherId = publisherId;
            SlotTypeId = request.SlotTypeId;
            Name = request.Name;
            Description = request.Description;
        }
    }
}
