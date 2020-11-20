using MediatR;

namespace Ookbee.Ads.Application.Services.Cache.Commands.DeleteUnavailableAdUnitCache
{
    public class DeleteUnavailableAdUnitCacheCommand : IRequest<Unit>
    {
        public long AdGroupId { get; private set; }

        public DeleteUnavailableAdUnitCacheCommand()
        {
            
        }
    }
}
