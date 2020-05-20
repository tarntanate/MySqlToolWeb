using MediatR;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.Ad.Queries.GetAdList
{
    public class GetAdListQuery : IRequest<HttpResult<IEnumerable<AdDto>>>
    {
        public string CampaignId { get; set; }

        public int Start { get; set; }
        
        public int Length { get; set; }

        public GetAdListQuery(string campaignId, int start, int length)
        {
            CampaignId = campaignId;
            Start = start;
            Length = length;
        }
    }
}
