using MediatR;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.Campaign.Queries.GetCampaignList
{
    public class GetCampaignListCommand : IRequest<HttpResult<IEnumerable<CampaignDto>>>
    {
        public int Start { get; set; }
        public int Length { get; set; }

        public GetCampaignListCommand(int start, int length)
        {
            Start = start;
            Length = length;
        }
    }
}
