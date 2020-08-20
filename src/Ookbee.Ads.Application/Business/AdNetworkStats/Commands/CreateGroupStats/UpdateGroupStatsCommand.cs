using MediatR;
using Ookbee.Ads.Infrastructure.Enums;

namespace Ookbee.Ads.Application.Business.AdNetworkStats.Commands.CreateGroupStats
{
    public class CreateGroupStatsCommand : IRequest<Unit>
    {
        public long AdGroupId { get; set; }
        public AdStats Stats { get; set; }

        public CreateGroupStatsCommand(long adGroupId, AdStats stats)
        {
            AdGroupId = adGroupId;
            Stats = stats;
        }
    }
}
