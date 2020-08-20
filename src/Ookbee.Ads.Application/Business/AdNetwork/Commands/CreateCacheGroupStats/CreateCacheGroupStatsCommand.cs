using MediatR;
using Ookbee.Ads.Infrastructure.Enums;

namespace Ookbee.Ads.Application.Business.AdNetwork.Commands.CreateCacheGroupStats
{
    public class CreateCacheGroupStatsCommand : IRequest<Unit>
    {
        public long AdGroupId { get; set; }
        public AdStats Stats { get; set; }

        public CreateCacheGroupStatsCommand(long adGroupId, AdStats stats)
        {
            AdGroupId = adGroupId;
            Stats = stats;
        }
    }
}
