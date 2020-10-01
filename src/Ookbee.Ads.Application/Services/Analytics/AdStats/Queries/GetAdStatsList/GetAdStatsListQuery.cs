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
        public long? AdId { get; set; }
        public DateTimeOffset? CaculatedAt { get; set; }

        public GetAdStatsListQuery(int start, int length, long? adId, DateTimeOffset? caculatedAt)
        {
            Start = start;
            Length = length;
            AdId = adId;
            CaculatedAt = caculatedAt;
        }
    }
}
