using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdSlot.Commands.DeleteAdSlot
{
    public class DeleteAdSlotCommand : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public string PublisherId { get; set; }

        public DeleteAdSlotCommand(string publisherId, string id)
        {
            Id = id;
            PublisherId = publisherId;
        }
    }
}
