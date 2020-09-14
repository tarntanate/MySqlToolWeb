using MediatR;
using Ookbee.Ads.Infrastructure.Models;
using System;

namespace Ookbee.Ads.Application.Business.Cache.AdStatsCache.Commands.CreateAdStatsByPlatformCache
{
    public class CreateAdStatsByPlatformCacheCommand : IRequest<Unit>
    {
        public DateTime CaculatedAt { get; set; }
        public Platform Platform { get; set; }
        public StatsType StatsType { get; set; }
        public long AdId { get; set; }
        public long Value { get; set; }

        public CreateAdStatsByPlatformCacheCommand(DateTime caculatedAt, Platform platform, StatsType statsType, long adId, long value)
        {
            CaculatedAt = caculatedAt;
            Platform = platform;
            StatsType = statsType;
            AdId = adId;
            Value = value;
        }
    }
}
