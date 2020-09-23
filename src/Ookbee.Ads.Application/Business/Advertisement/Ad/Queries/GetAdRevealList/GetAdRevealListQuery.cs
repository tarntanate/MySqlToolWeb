using MediatR;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.Advertisement.Ad.Queries.GetAdRevealList
{
    public class GetAdRevealListQuery : IRequest<Response<IEnumerable<AdDto>>>
    {
        public long? AdUnitId { get; set; }
        public long? CampaignId { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }

        public GetAdRevealListQuery(int start, int length, long? adUnitId, long? campaignId)
        {
            Start = start;
            Length = length;
            AdUnitId = adUnitId;
            CampaignId = campaignId;
        }
    }
}
