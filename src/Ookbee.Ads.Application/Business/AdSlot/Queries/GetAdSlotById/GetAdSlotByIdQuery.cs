using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdSlot.Queries.GetAdSlotById
{
    public class GetAdSlotByIdQuery : IRequest<HttpResult<AdSlotDto>>
    {
        public string Id { get; set; }

        public string PublisherId { get; set; }

        public GetAdSlotByIdQuery(string id)
        {
            Id = id;
        }

        public GetAdSlotByIdQuery(string publisherId, string id)
        {
            Id = id;
            PublisherId = publisherId;
        }
    }
}
