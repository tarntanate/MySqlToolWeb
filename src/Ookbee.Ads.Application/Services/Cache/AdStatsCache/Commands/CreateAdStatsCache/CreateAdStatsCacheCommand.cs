using MediatR;
using Ookbee.Ads.Infrastructure.Models;
using System;

namespace Ookbee.Ads.Application.Services.Cache.AdStatsCache.Commands.CreateAdStatsCache
{
    public class CreateAdStatsCacheCommand : IRequest<Unit>
    {
        public DateTimeOffset CaculatedAt { get; set; }
        public AdStatsType StatsType { get; set; }
        public long AdId { get; set; }
        public long Value { get; set; }

        public CreateAdStatsCacheCommand(DateTimeOffset caculatedAt, AdStatsType statsType, long adId, long value)
        {
            CaculatedAt = caculatedAt;
            StatsType = statsType;
            AdId = adId;
            Value = value;
        }
    }
}
