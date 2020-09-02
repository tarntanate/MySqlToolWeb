using MediatR;
using Ookbee.Ads.Infrastructure.Models;
using System;

namespace Ookbee.Ads.Application.Business.Cache.AdGroupStatsCache.Commands.CreateAdGroupStatCache
{
    public class CreateAdGroupStatsCacheCommand : IRequest<Unit>
    {
        public long AdGroupId { get; set; }
        public Platform Platform { get; set; }
        public AdStats Stats { get; set; }
        public long Value { get; set; }
        public DateTime CaculatedAt { get; set; }

    }
}
