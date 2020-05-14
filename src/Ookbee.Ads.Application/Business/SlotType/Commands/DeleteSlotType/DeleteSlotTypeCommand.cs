using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.SlotType.Commands.DeleteSlotType
{
    public class DeleteSlotTypeCommand : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public DeleteSlotTypeCommand(string id)
        {
            Id = id;
        }
    }
}
