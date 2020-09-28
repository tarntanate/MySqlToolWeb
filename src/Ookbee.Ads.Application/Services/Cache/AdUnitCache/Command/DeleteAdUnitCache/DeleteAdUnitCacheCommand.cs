using MediatR;

namespace Ookbee.Ads.Application.Services.Cache.AdUnitCache.Commands.DeleteAdUnitCache
{
    public class DeleteAdUnitCacheCommand : IRequest<Unit>
    {
        public long AdUnitId { get; set; }

        public DeleteAdUnitCacheCommand(long adUnitId)
        {
            AdUnitId = adUnitId;
        }
    }
}
