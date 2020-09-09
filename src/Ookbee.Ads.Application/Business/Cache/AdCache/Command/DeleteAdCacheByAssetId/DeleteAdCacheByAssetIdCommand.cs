using MediatR;

namespace Ookbee.Ads.Application.Business.Cache.AdAssetCache.Commands.DeleteAdAssetCacheByAssetId
{
    public class DeleteAdAssetCacheByAssetIdCommand : IRequest<Unit>
    {
        public long AdId { get; set; }

        public DeleteAdAssetCacheByAssetIdCommand(long adId)
        {
            AdId = adId;
        }
    }
}
