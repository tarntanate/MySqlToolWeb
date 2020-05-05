using MediatR;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.CampaignAdvertiser.Queries.GetListCampaignAdvertiser
{
    public class GetListCampaignAdvertiserCommand : IRequest<HttpResult<IEnumerable<CampaignAdvertiserDto>>>
    {
        public int Start { get; set; }
        public int Length { get; set; }

        public GetListCampaignAdvertiserCommand(int start, int length)
        {
            Start = start;
            Length = length;
        }
    }
}
