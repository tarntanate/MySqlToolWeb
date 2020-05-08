using MediatR;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.CampaignItem.Queries.GetListCampaignItem
{
    public class GetListCampaignItemCommand : IRequest<HttpResult<IEnumerable<CampaignItemDto>>>
    {
        public int Start { get; set; }
        public int Length { get; set; }

        public GetListCampaignItemCommand(int start, int length)
        {
            Start = start;
            Length = length;
        }
    }
}
