using MediatR;
using Ookbee.Ads.Common.Response;
using System;

namespace Ookbee.Ads.Application.Services.Analytics.AdUnitStats.Queries.GetAdUnitStats
{
    public class GetAdUnitStatsQuery : IRequest<Response<AdUnitStatsDto>>
    {
        public DateTimeOffset CaculatedAt { get; set; }
        public long AdUnitId { get; set; }

        public GetAdUnitStatsQuery(DateTimeOffset caculatedAt, long adUnitId)
        {
            CaculatedAt = caculatedAt;
            AdUnitId = adUnitId;
        }
    }
}
