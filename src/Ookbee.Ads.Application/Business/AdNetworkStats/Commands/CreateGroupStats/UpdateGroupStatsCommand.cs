using MediatR;
using Ookbee.Ads.Infrastructure.Enums;

namespace Ookbee.Ads.Application.Business.AdNetworkStats.Commands.UpdateGroupStats
{
    public class UpdateGroupStatsCommand : IRequest<Unit>
    {
        public long AdGroupId { get; set; }
        public AdStats Stats { get; set; }

        public UpdateGroupStatsCommand(long adGroupId, AdStats stats)
        {
            AdGroupId = adGroupId;
            Stats = stats;
        }
    }
}
