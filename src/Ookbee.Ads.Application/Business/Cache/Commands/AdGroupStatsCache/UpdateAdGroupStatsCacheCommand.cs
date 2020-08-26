using MediatR;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Business.Cache.Commands.AdGroupStatsCache
{
    public class UpdateAdGroupStatsCacheCommand : IRequest<Unit>
    {
        public long AdGroupId { get; set; }
        public Platform Platform { get; set; }
        public AdStats Stats { get; set; }

        public UpdateAdGroupStatsCacheCommand(long adGroupId, Platform platform, AdStats stats)
        {
            AdGroupId = adGroupId;
            Platform = platform;
            Stats = stats;
        }
    }
}
