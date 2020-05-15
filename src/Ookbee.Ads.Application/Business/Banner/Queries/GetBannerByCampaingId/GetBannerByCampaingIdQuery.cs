using System.Collections.Generic;
using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Banner.Queries.GetBannerByCampaingId
{
    public class GetBannerByCampaingIdQuery : IRequest<HttpResult<IEnumerable<BannerDto>>>
    {
        public string CampaingId { get; set; }

        public int Start { get; set; }

        public int Length { get; set; }

        public GetBannerByCampaingIdQuery(string campaingId, int start, int length)
        {
            CampaingId = campaingId;
            Start = start;
            Length = length;
        }
    }
}
