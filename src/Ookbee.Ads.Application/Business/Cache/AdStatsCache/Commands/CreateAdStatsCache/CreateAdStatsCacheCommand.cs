using MediatR;
using Ookbee.Ads.Infrastructure.Models;
using System;

namespace Ookbee.Ads.Application.Business.Cache.AdStatsCache.Commands.CreateAdStatsCache
{
    public class CreateAdStatsCacheCommand : IRequest<Unit>
    {
        public long AdId { get; set; }
        public Platform Platform { get; set; }
        public DateTime CaculatedAt { get; set; }
        public StatsType StatsType { get; set; }
        public long Value { get; set; }

        public CreateAdStatsCacheCommand(long adId, Platform platform, DateTime caculatedAt, StatsType statsType, long value)
        {
            AdId = adId;
            Platform = platform;
            CaculatedAt = caculatedAt;
            StatsType = statsType;
            Value = value;
        }
    }
}
