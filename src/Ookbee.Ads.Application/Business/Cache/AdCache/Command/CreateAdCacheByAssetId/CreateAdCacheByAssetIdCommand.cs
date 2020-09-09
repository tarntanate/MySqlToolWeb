using MediatR;

namespace Ookbee.Ads.Application.Business.Cache.AdAssetCache.Commands.CeateAdAssetCacheByAssetId
{
    public class CreateAdAssetCacheByAssetIdCommand : IRequest<Unit>
    {
        public long AdId { get; set; }

        public CreateAdAssetCacheByAssetIdCommand(long adId)
        {
            AdId = adId;
        }
    }
}
