using System.Collections.Generic;
using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Ad.Queries.GetAdByCampaingId
{
    public class GetAdByCampaingIdQuery : IRequest<HttpResult<IEnumerable<AdDto>>>
    {
        public string CampaingId { get; set; }

        public int Start { get; set; }

        public int Length { get; set; }

        public GetAdByCampaingIdQuery(string campaingId, int start, int length)
        {
            CampaingId = campaingId;
            Start = start;
            Length = length;
        }
    }
}
