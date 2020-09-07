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
        public AdStatsType Stats { get; set; }
        public long Value { get; set; }

        public CreateAdGroupStatsCacheCommand(long adGroupId, Platform platform, DateTime caculatedAt, AdStatsType stats, long value)
        {
            AdGroupId = adGroupId;
            Platform = platform;
            CaculatedAt = caculatedAt;
            Stats = stats;
            Value = value;
        }
    }
}
