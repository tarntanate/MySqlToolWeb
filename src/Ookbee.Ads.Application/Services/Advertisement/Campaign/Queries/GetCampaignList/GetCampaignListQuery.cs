using MediatR;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Advertisement.Campaign.Queries.GetCampaignList
{
    public class GetCampaignListQuery : IRequest<Response<IEnumerable<CampaignDto>>>
    {
        public long? AdvertiserId { get; private set; }
        public int Start { get; private set; }
        public int Length { get; private set; }

        public GetCampaignListQuery(int start, int length, long? advertiserId)
        {
            AdvertiserId = advertiserId;
            Start = start;
            Length = length;
        }
    }
}
