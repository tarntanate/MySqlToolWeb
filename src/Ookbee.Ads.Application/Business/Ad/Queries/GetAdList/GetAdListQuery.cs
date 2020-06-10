using MediatR;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.Ad.Queries.GetAdList
{
    public class GetAdListQuery : IRequest<HttpResult<IEnumerable<AdDto>>>
    {
        public long? AdUnitId { get; set; }
        public long? CampaignId { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }

        public GetAdListQuery(long adTypeId, long publisherId, int start, int length)
        {
            AdUnitId = adTypeId;
            CampaignId = publisherId;
            Start = start;
            Length = length;
        }
    }
}
