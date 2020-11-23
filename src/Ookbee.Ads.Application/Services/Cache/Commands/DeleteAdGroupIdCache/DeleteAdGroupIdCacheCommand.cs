using MediatR;

namespace Ookbee.Ads.Application.Services.Cache.Commands.DeleteAdGroupIdCache
{
    public class DeleteAdGroupIdCacheCommand : IRequest<Unit>
    {
        public long AdGroupId { get; private set; }
        
        public DeleteAdGroupIdCacheCommand(long adGroupId)
        {
            AdGroupId = adGroupId;
        }
    }
}
