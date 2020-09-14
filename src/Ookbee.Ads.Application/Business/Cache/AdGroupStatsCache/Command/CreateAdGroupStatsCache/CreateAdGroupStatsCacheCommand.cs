﻿using MediatR;
using Ookbee.Ads.Infrastructure.Models;
using System;

namespace Ookbee.Ads.Application.Business.Cache.AdGroupStatsCache.Commands.CreateAdGroupStatsCache
{
    public class CreateAdGroupStatsCacheCommand : IRequest<Unit>
    {
        public DateTime CaculatedAt { get; set; }
        public Platform Platform { get; set; }
        public StatsType StatsType { get; set; }
        public long AdGroupId { get; set; }
        public long Value { get; set; }

        public CreateAdGroupStatsCacheCommand(DateTime caculatedAt, Platform platform, StatsType statsType, long adGroupId, long value)
        {
            CaculatedAt = caculatedAt;
            Platform = platform;
            StatsType = statsType;
            AdGroupId = adGroupId;
            Value = value;
        }
    }
}
