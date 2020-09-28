using MediatR;
using Ookbee.Ads.Common.Response;
using System;

namespace Ookbee.Ads.Application.Services.Analytics.AdUnitStats.Queries.GetAdUnitStatsByKey
{
    public class GetAdUnitStatsByKeyQuery : IRequest<Response<AdUnitStatsDto>>
    {
        public long AdUnitId { get; set; }
        public DateTimeOffset CaculatedAt { get; set; }

        public GetAdUnitStatsByKeyQuery(long adUnitId, DateTimeOffset caculatedAt)
        {
            AdUnitId = adUnitId;
            CaculatedAt = caculatedAt;
        }
    }
}
