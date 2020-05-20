using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Ad.Queries.IsExistsAdByName
{
    public class IsExistsAdByNameQuery : IRequest<HttpResult<bool>>
    {
        public string AdSlotId { get; set; }

        public string Name { get; set; }

        public IsExistsAdByNameQuery(string adSlotId, string name)
        {
            AdSlotId = adSlotId;
            Name = name;
        }
    }
}
