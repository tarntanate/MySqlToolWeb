using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdSlot.Queries.GetAdSlotById
{
    public class GetAdSlotByIdQuery : IRequest<HttpResult<AdSlotDto>>
    {
        public string Id { get; set; }

        public GetAdSlotByIdQuery(string id)
        {
            Id = id;
        }
    }
}
