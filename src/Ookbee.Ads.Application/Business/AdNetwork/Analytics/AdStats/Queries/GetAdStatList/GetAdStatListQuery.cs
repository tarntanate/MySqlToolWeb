using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Infrastructure.Models;
using System;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.AdNetwork.Analytics.AdStatsList.Queries.GetAdStatsList
{
    public class GetAdStatsListQuery : IRequest<HttpResult<IEnumerable<AdStatsDto>>>
    {
        public int Start { get; set; }
        public int Length { get; set; }
        public long AdId { get; set; }
        public Platform Platform { get; set; }
        public DateTime CaculatedAt { get; set; }

        public GetAdStatsListQuery(int start, int length, long adGroupId, Platform platform, DateTime caculatedAt)
        {
            Start = start;
            Length = length;
            AdId = adGroupId;
            Platform = platform;
            CaculatedAt = caculatedAt;
        }
    }
}
