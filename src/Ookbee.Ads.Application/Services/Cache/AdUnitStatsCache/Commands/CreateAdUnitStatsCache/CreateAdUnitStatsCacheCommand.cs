using MediatR;
using Ookbee.Ads.Infrastructure.Models;
using System;

namespace Ookbee.Ads.Application.Services.Cache.AdUnitStatsCache.Commands.CreateAdUnitStatsCache
{
    public class CreateAdUnitStatsCacheCommand : IRequest<Unit>
    {
        public DateTimeOffset CaculatedAt { get; set; }
        public long AdUnitId { get; set; }
        public StatsType StatsType { get; set; }
        public long Value { get; set; }

        public CreateAdUnitStatsCacheCommand(DateTimeOffset caculatedAt, long adUnitId, StatsType statsType, long value)
        {
            CaculatedAt = caculatedAt;
            AdUnitId = adUnitId;
            StatsType = statsType;
            Value = value;
        }
    }
}
