using MediatR;
using Ookbee.Ads.Application.Services.Analytics.AdStats;
using Ookbee.Ads.Common.Response;
using System;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Analytics.AdStat.Queries.GetAdStatsList
{
    public class GetAdStatsListQuery : IRequest<Response<IEnumerable<AdStatsDto>>>
    {
        public int Start { get; set; }
        public int Length { get; set; }
        public DateTimeOffset? CaculatedAt { get; set; }
        public long? AdUnitId { get; set; }
        public long? AdId { get; set; }

        public GetAdStatsListQuery(int start, int length, DateTimeOffset? caculatedAt, long? adUnitId, long? adId)
        {
            Start = start;
            Length = length;
            CaculatedAt = caculatedAt;
            AdUnitId = adUnitId;
            AdId = adId;
        }
    }
}
