using MediatR;

namespace Ookbee.Ads.Application.Business.Cache.AdAssetCache.Commands.UpdateAdAssetCacheByAssetId
{
    public class UpdateAdAssetCacheByAssetIdCommand : IRequest<Unit>
    {
        public long AdId { get; set; }

        public UpdateAdAssetCacheByAssetIdCommand(long adId)
        {
            AdId = adId;
        }
    }
}
