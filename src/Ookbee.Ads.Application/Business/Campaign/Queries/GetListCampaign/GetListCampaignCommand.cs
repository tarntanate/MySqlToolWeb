using MediatR;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.Campaign.Queries.GetListCampaign
{
    public class GetListCampaignCommand : IRequest<HttpResult<IEnumerable<CampaignDto>>>
    {
        public int Start { get; set; }
        public int Length { get; set; }

        public GetListCampaignCommand(int start, int length)
        {
            Start = start;
            Length = length;
        }
    }
}
