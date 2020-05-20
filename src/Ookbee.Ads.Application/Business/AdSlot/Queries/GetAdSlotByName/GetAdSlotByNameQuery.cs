using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdSlot.Queries.GetAdSlotByName
{
    public class GetAdSlotByNameQuery : IRequest<HttpResult<AdSlotDto>>
    {
        public string PublisherId { get; set; }

        public string Name { get; set; }

        public GetAdSlotByNameQuery(string publisherId, string name)
        {
            Name = name;
            PublisherId = publisherId;
        }
    }
}
