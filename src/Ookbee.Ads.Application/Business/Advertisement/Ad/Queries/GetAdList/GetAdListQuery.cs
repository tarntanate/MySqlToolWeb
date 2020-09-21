using MediatR;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.Advertisement.Ad.Queries.GetAdList
{
    public class GetAdListQuery : IRequest<HttpResult<IEnumerable<AdDto>>>
    {
        public int Start { get; set; }
        public int Length { get; set; }
        public long? AdUnitId { get; set; }
        public long? CampaignId { get; set; }

        public GetAdListQuery(int start, int length, long? adUnitId, long? campaignId)
        {
            Start = start;
            Length = length;
            AdUnitId = adUnitId;
            CampaignId = campaignId;
        }
    }
}
