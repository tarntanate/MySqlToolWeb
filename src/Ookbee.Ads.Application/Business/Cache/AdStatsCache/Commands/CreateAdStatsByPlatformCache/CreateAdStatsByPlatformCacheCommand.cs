﻿using MediatR;
using Ookbee.Ads.Infrastructure.Models;
using System;

namespace Ookbee.Ads.Application.Business.Cache.AdStatsCache.Commands.CreateAdStatsByPlatformCache
{
    public class CreateAdStatsByPlatformCacheCommand : IRequest<Unit>
    {
        public DateTimeOffset CaculatedAt { get; set; }
        public StatsType StatsType { get; set; }
        public long AdId { get; set; }
        public long Value { get; set; }

        public CreateAdStatsByPlatformCacheCommand(DateTimeOffset caculatedAt, StatsType statsType, long adId, long value)
        {
            CaculatedAt = caculatedAt;
            StatsType = statsType;
            AdId = adId;
            Value = value;
        }
    }
}
