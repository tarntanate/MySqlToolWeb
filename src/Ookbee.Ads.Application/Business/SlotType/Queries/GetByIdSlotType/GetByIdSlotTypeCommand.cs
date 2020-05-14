using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.SlotType.Queries.GetByIdSlotType
{
    public class GetByIdSlotTypeCommand : IRequest<HttpResult<SlotTypeDto>>
    {
        public string Id { get; set; }

        public GetByIdSlotTypeCommand(string id)
        {
            Id = id;
        }
    }
}
