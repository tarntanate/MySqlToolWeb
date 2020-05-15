using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.SlotType.Queries.GetSlotTypeById
{
    public class GetSlotTypeByIdQuery : IRequest<HttpResult<SlotTypeDto>>
    {
        public string Id { get; set; }

        public GetSlotTypeByIdQuery(string id)
        {
            Id = id;
        }
    }
}
