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
        public AdStatsType Stats { get; set; }
        public long Value { get; set; }

        public CreateAdUnitStatsCacheCommand(long adUnitId, Platform platform, DateTime caculatedAt, AdStatsType stats, long value)
        {
            AdUnitId = adUnitId;
            Platform = platform;
            CaculatedAt = caculatedAt;
            Stats = stats;
            Value = value;
        }
    }
}
