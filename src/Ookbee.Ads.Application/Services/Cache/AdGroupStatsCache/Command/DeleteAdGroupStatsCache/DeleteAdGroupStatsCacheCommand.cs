using MediatR;

namespace Ookbee.Ads.Application.Services.Cache.AdGroupStatsCache.Commands.DeleteAdGroupStatsCache
{
    public class DeleteAdGroupStatsCacheCommand : IRequest<Unit>
    {
        public long AdGroupId { get; set; }

        public DeleteAdGroupStatsCacheCommand(long adGroupId)
        {
            AdGroupId = adGroupId;
        }
    }
}
