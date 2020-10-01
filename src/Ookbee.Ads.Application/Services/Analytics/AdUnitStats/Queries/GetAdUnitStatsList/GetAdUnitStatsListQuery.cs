using MediatR;
using Ookbee.Ads.Common.Response;
using System;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Analytics.AdUnitStats.Queries.GetAdUnitStatsList
{
    public class GetAdUnitStatsListQuery : IRequest<Response<IEnumerable<AdUnitStatsDto>>>
    {
        public int Start { get; set; }
        public int Length { get; set; }
        public long? AdUnitId { get; set; }
        public DateTimeOffset? CaculatedAt { get; set; }

        public GetAdUnitStatsListQuery(int start, int length, long? adUnitId, DateTimeOffset? caculatedAt)
        {
            Start = start;
            Length = length;
            AdUnitId = adUnitId;
            CaculatedAt = caculatedAt;
        }
    }
}
