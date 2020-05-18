using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Ad.Queries.IsExistsAdByName
{
    public class IsExistsAdByNameQuery : IRequest<HttpResult<bool>>
    {
        public string CampaignId { get; set; }

        public string AdSlotId { get; set; }

        public string Name { get; set; }

        public IsExistsAdByNameQuery(string campaignId, string adSlotId, string name)
        {
            CampaignId = campaignId;
            AdSlotId = adSlotId;
            Name = name;
        }
    }
}
