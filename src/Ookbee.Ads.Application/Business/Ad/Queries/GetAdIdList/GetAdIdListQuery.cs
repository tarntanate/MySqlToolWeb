using MediatR;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.Ad.Queries.GetAdIdList
{
    public class GetAdIdListQuery : IRequest<HttpResult<IEnumerable<long>>>
    {
        public long? AdUnitId { get; set; }
        public long? CampaignId { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }

        public GetAdIdListQuery(int start, int length, long? adUnitId, long? campaignId)
        {
            Start = start;
            Length = length;
            AdUnitId = adUnitId;
            CampaignId = campaignId;
        }
    }
}
