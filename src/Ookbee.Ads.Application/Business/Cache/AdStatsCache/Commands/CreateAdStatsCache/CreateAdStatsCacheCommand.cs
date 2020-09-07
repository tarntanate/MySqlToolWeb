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
        public AdStatsType Stats { get; set; }
        public long Value { get; set; }

        public CreateAdStatsCacheCommand(long adId, Platform platform, DateTime caculatedAt, AdStatsType stats, long value)
        {
            AdId = adId;
            Platform = platform;
            CaculatedAt = caculatedAt;
            Stats = stats;
            Value = value;
        }
    }
}
