using MediatR;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.CampaignItemAsset.Queries.GetListCampaignItemAsset
{
    public class GetListCampaignItemAssetCommand : IRequest<HttpResult<IEnumerable<CampaignItemAssetDto>>>
    {
        public int Start { get; set; }
        public int Length { get; set; }

        public GetListCampaignItemAssetCommand(int start, int length)
        {
            Start = start;
            Length = length;
        }
    }
}
