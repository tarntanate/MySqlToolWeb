using MediatR;
using Ookbee.Ads.Common.Response;
using System;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Analytics.AdGroupStat.Queries.GetAdGroupStatsList
{
    public class GetAdGroupStatsListQuery : IRequest<Response<IEnumerable<AdGroupStatsDto>>>
    {
        public int Start { get; set; }
        public int Length { get; set; }
        public long? AdGroupId { get; set; }
        public DateTimeOffset? CaculatedAt { get; set; }

        public GetAdGroupStatsListQuery(int start, int length, long? adGroupId, DateTimeOffset? caculatedAt)
        {
            Start = start;
            Length = length;
            AdGroupId = adGroupId;
            CaculatedAt = caculatedAt;
        }
    }
}
