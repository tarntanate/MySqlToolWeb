using MediatR;
using Ookbee.Ads.Common.Response;
using System;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Analytics.AdGroupStat.Queries.GetAdGroupStatsIdList
{
    public class GetAdGroupStatsIdListQuery : IRequest<Response<IEnumerable<long>>>
    {
        public int Start { get; set; }
        public int Length { get; set; }
        public long? AdGroupId { get; set; }
        public DateTimeOffset? CaculatedAt { get; set; }

        public GetAdGroupStatsIdListQuery(int start, int length, long? adGroupId, DateTimeOffset? caculatedAt)
        {
            Start = start;
            Length = length;
            AdGroupId = adGroupId;
            CaculatedAt = caculatedAt;
        }
    }
}
