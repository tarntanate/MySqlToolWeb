using MediatR;
using Ookbee.Ads.Common.Response;
using System;

namespace Ookbee.Ads.Application.Business.Analytics.AdStats.Queries.GetAdStatsByKey
{
    public class GetAdStatsByKeyQuery : IRequest<Response<AdStatsDto>>
    {
        public long AdId { get; set; }
        public DateTimeOffset CaculatedAt { get; set; }

        public GetAdStatsByKeyQuery(long adId, DateTimeOffset caculatedAt)
        {
            AdId = adId;
            CaculatedAt = caculatedAt;
        }
    }
}
