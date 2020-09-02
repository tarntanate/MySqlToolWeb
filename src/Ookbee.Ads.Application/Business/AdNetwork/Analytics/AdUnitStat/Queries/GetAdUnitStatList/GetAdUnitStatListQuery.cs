using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Infrastructure.Models;
using System;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.AdNetwork.Analytics.AdUnitStatsList.Queries.GetAdUnitStatsList
{
    public class GetAdUnitStatsListQuery : IRequest<HttpResult<IEnumerable<AdUnitStatsDto>>>
    {
        public int Start { get; set; }
        public int Length { get; set; }
        public long AdUnitId { get; set; }
        public Platform Platform { get; set; }
        public DateTime CaculatedAt { get; set; }

        public GetAdUnitStatsListQuery(int start, int length, long adGroupId, Platform platform, DateTime caculatedAt)
        {
            Start = start;
            Length = length;
            AdUnitId = adGroupId;
            Platform = platform;
            CaculatedAt = caculatedAt;
        }
    }
}
