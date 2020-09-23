using MediatR;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Advertisement.Ad.Queries.GetAdList
{
    public class GetAdListQuery : IRequest<Response<IEnumerable<AdDto>>>
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
