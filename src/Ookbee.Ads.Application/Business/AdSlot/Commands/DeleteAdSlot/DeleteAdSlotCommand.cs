using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdSlot.Commands.DeleteAdSlot
{
    public class DeleteAdSlotCommand : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public DeleteAdSlotCommand(string id)
        {
            Id = id;
        }
    }
}
