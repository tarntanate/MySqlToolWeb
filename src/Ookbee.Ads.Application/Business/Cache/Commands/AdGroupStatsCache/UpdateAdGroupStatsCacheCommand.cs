using MediatR;
using Ookbee.Ads.Infrastructure.Enums;

namespace Ookbee.Ads.Application.Business.Cache.Commands.AdGroupStatsCache
{
    public class UpdateAdGroupStatsCacheCommand : IRequest<Unit>
    {
        public long AdGroupId { get; set; }
        public AdStats Stats { get; set; }

        public UpdateAdGroupStatsCacheCommand(long adGroupId, AdStats stats)
        {
            AdGroupId = adGroupId;
            Stats = stats;
        }
    }
}
