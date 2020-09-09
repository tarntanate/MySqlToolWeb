using MediatR;
using Ookbee.Ads.Infrastructure.Models;
using System;

namespace Ookbee.Ads.Application.Business.Cache.AdUnitStatsCache.Commands.CreateAdUnitStatsCache
{
    public class CreateAdUnitStatsCacheCommand : IRequest<Unit>
    {
        public long AdUnitId { get; set; }
        public Platform Platform { get; set; }
        public DateTime CaculatedAt { get; set; }
        public StatsType StatsType { get; set; }
        public long Value { get; set; }

        public CreateAdUnitStatsCacheCommand(long adUnitId, Platform platform, DateTime caculatedAt, StatsType statsType, long value)
        {
            AdUnitId = adUnitId;
            Platform = platform;
            CaculatedAt = caculatedAt;
            StatsType = statsType;
            Value = value;
        }
    }
}
