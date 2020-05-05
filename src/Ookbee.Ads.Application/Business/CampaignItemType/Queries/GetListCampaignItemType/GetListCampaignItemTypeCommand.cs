using MediatR;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.CampaignItemType.Queries.GetListCampaignItemType
{
    public class GetListCampaignItemTypeCommand : IRequest<HttpResult<IEnumerable<CampaignItemTypeDto>>>
    {
        public int Start { get; set; }
        public int Length { get; set; }

        public GetListCampaignItemTypeCommand(int start, int length)
        {
            Start = start;
            Length = length;
        }
    }
}
