﻿using MediatR;
using Ookbee.Ads.Infrastructure.Models;
using System;

namespace Ookbee.Ads.Application.Services.Cache.AdGroupStatsCache.Commands.CreateAdGroupStatsCache
{
    public class CreateAdGroupStatsCacheCommand : IRequest<Unit>
    {
        public DateTimeOffset CaculatedAt { get; set; }
        public AdStatsType StatsType { get; set; }
        public long AdGroupId { get; set; }
        public long Value { get; set; }

        public CreateAdGroupStatsCacheCommand(DateTimeOffset caculatedAt, AdStatsType statsType, long adGroupId, long value)
        {
            CaculatedAt = caculatedAt;
            StatsType = statsType;
            AdGroupId = adGroupId;
            Value = value;
        }
    }
}
