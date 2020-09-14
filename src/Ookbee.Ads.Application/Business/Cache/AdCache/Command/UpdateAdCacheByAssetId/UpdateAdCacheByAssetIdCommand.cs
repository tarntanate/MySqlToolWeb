using MediatR;

namespace Ookbee.Ads.Application.Business.Cache.AdCache.Commands.UpdateAdCacheByAssetId
{
    public class UpdateAdCacheByAssetIdCommand : IRequest<Unit>
    {
        public long AdAssetId { get; set; }

        public UpdateAdCacheByAssetIdCommand(long adAssetId)
        {
            AdAssetId = adAssetId;
        }
    }
}
