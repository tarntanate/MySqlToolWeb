using System.Collections.Generic;
using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Ad.Queries.GetAdByCampaignId
{
    public class GetAdByCampaignIdQuery : IRequest<HttpResult<IEnumerable<AdDto>>>
    {
        public string CampaignId { get; set; }

        public int Start { get; set; }

        public int Length { get; set; }

        public GetAdByCampaignIdQuery(string campaignId, int start, int length)
        {
            CampaignId = campaignId;
            Start = start;
            Length = length;
        }
    }
}
