using MediatR;

namespace Ookbee.Ads.Application.Services.Cache.Commands.DeleteAdGroupUnavailableCache
{
    public class DeleteAdGroupUnavailableCacheCommand : IRequest<Unit>
    {
        public long AdGroupId { get; private set; }
        public DeleteAdGroupUnavailableCacheCommand(long adGroupId)
        {
            AdGroupId = adGroupId;
        }
    }
}
