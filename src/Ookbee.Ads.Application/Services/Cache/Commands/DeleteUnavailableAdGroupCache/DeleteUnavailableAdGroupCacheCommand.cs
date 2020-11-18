using MediatR;

namespace Ookbee.Ads.Application.Services.Cache.Commands.DeleteUnavailableAdGroupCache
{
    public class DeleteUnavailableAdGroupCacheCommand : IRequest<Unit>
    {
        public long AdGroupId { get; private set; }
        public DeleteUnavailableAdGroupCacheCommand(long adGroupId)
        {
            AdGroupId = adGroupId;
        }
    }
}
