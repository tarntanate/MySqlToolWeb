using MediatR;

namespace Ookbee.Ads.Application.Business.Cache.AdAssetCache.Commands.DeleteAdAssetCache
{
    public class DeleteAdAssetCacheCommand : IRequest<Unit>
    {
        public long AdId { get; set; }

        public DeleteAdAssetCacheCommand(long adId)
        {
            AdId = adId;
        }
    }
}
