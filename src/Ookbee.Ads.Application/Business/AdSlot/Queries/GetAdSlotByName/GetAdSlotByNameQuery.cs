using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdSlot.Queries.GetAdSlotByName
{
    public class GetAdSlotByNameQuery : IRequest<HttpResult<AdSlotDto>>
    {
        public string Name { get; set; }

        public GetAdSlotByNameQuery(string name)
        {
            Name = name;
        }
    }
}
