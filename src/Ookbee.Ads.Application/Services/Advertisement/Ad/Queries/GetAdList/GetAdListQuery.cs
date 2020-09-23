using MediatR;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Advertisement.Ad.Queries.GetAdList
{
    public class GetAdListQuery : IRequest<Response<IEnumerable<AdDto>>>
    {
        public int Start { get; private set; }
        public int Length { get; private set; }
        public long? AdUnitId { get; private set; }
        public long? CampaignId { get; private set; }

        public GetAdListQuery(int start, int length, long? adUnitId, long? campaignId)
        {
            Start = start;
            Length = length;
            AdUnitId = adUnitId;
            CampaignId = campaignId;
        }
    }
}
