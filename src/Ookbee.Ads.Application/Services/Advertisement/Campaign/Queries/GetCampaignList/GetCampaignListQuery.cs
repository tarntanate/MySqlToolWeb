using MediatR;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Advertisement.Campaign.Queries.GetCampaignList
{
    public class GetCampaignListQuery : IRequest<Response<IEnumerable<CampaignDto>>>
    {
        public int Start { get; private set; }
        public int Length { get; private set; }
        public long? AdvertiserId { get; private set; }

        public GetCampaignListQuery(int start, int length, long? advertiserId)
        {
            Start = start;
            Length = length;
            AdvertiserId = advertiserId;
        }
    }
}
