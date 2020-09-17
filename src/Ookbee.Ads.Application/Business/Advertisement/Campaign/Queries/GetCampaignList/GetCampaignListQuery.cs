using MediatR;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.Advertisement.Campaign.Queries.GetCampaignList
{
    public class GetCampaignListQuery : IRequest<HttpResult<IEnumerable<CampaignDto>>>
    {
        public long? AdvertiserId { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }

        public GetCampaignListQuery(int start, int length, long? advertiserId)
        {
            AdvertiserId = advertiserId;
            Start = start;
            Length = length;
        }
    }
}
