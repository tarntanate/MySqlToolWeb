using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.SlotType.Queries.IsExistsSlotTypeById
{
    public class IsExistsSlotTypeByIdQuery : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public IsExistsSlotTypeByIdQuery(string id)
        {
            Id = id;
        }
    }
}
