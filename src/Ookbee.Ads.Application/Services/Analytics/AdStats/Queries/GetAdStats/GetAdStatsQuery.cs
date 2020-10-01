using MediatR;
using Ookbee.Ads.Common.Response;
using System;

namespace Ookbee.Ads.Application.Services.Analytics.AdStats.Queries.GetAdStats
{
    public class GetAdStatsQuery : IRequest<Response<AdStatsDto>>
    {
        public long AdId { get; set; }
        public DateTimeOffset CaculatedAt { get; set; }

        public GetAdStatsQuery(long adId, DateTimeOffset caculatedAt)
        {
            AdId = adId;
            CaculatedAt = caculatedAt;
        }
    }
}
