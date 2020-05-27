using MediatR;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.Ad.Queries.GetAdList
{
    public class GetAdListQuery : IRequest<HttpResult<IEnumerable<AdDto>>>
    {
        public string AdSlotId { get; set; }

        public string CampaignId { get; set; }

        public int Start { get; set; }

        public int Length { get; set; }

        public GetAdListQuery(string adSlotId, string campaignId, int start, int length)
        {
            AdSlotId = adSlotId;
            CampaignId = campaignId;
            Start = start;
            Length = length;
        }
    }
}
