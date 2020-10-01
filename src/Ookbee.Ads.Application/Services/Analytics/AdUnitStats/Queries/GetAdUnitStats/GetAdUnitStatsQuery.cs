using MediatR;
using Ookbee.Ads.Common.Response;
using System;

namespace Ookbee.Ads.Application.Services.Analytics.AdUnitStats.Queries.GetAdUnitStats
{
    public class GetAdUnitStatsQuery : IRequest<Response<AdUnitStatsDto>>
    {
        public long AdUnitId { get; set; }
        public DateTimeOffset CaculatedAt { get; set; }

        public GetAdUnitStatsQuery(long adUnitId, DateTimeOffset caculatedAt)
        {
            AdUnitId = adUnitId;
            CaculatedAt = caculatedAt;
        }
    }
}
