using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdSlot.Queries.IsExistsAdSlotById
{
    public class IsExistsAdSlotByIdQuery : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public IsExistsAdSlotByIdQuery(string id)
        {
            Id = id;
        }
    }
}
