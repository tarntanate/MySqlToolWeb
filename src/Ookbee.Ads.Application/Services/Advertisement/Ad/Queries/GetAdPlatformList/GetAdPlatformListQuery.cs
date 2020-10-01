using MediatR;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Advertisement.Ad.Queries.GetAdPlatformList
{
    public class GetAdPlatformListQuery : IRequest<Response<IEnumerable<AdPlatformDto>>>
    {
        public int Start { get; private set; }
        public int Length { get; private set; }
        public long? AdUnitId { get; private set; }
        public long? CampaignId { get; private set; }

        public GetAdPlatformListQuery(int start, int length, long? adUnitId, long? campaignId)
        {
            Start = start;
            Length = length;
            AdUnitId = adUnitId;
            CampaignId = campaignId;
        }
    }
}
