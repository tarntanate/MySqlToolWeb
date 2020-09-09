using MediatR;
using Ookbee.Ads.Infrastructure.Models;
using System;

namespace Ookbee.Ads.Application.Business.Cache.AdGroupStatsCache.Commands.CreateAdGroupStatsCache
{
    public class CreateAdGroupStatsCacheCommand : IRequest<Unit>
    {
        public long AdGroupId { get; set; }
        public Platform Platform { get; set; }
        public DateTime CaculatedAt { get; set; }
        public StatsType StatsType { get; set; }
        public long Value { get; set; }

        public CreateAdGroupStatsCacheCommand(long adGroupId, Platform platform, DateTime caculatedAt, StatsType statsType, long value)
        {
            AdGroupId = adGroupId;
            Platform = platform;
            CaculatedAt = caculatedAt;
            this.StatsType = statsType;
            Value = value;
        }
    }
}
