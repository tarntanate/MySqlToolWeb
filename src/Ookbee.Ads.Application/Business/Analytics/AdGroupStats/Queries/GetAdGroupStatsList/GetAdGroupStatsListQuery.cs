using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Infrastructure.Models;
using System;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.Analytics.AdGroupStatsList.Queries.GetAdGroupStatsList
{
    public class GetAdGroupStatsListQuery : IRequest<HttpResult<IEnumerable<AdGroupStatsDto>>>
    {
        public int Start { get; set; }
        public int Length { get; set; }
        public long AdGroupId { get; set; }
        public Platform Platform { get; set; }
        public DateTime CaculatedAt { get; set; }

        public GetAdGroupStatsListQuery(int start, int length, long adGroupId, Platform platform, DateTime caculatedAt)
        {
            Start = start;
            Length = length;
            AdGroupId = adGroupId;
            Platform = platform;
            CaculatedAt = caculatedAt;
        }
    }
}
