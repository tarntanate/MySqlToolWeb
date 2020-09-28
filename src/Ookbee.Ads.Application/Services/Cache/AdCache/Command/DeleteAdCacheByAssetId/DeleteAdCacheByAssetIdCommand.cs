using MediatR;

namespace Ookbee.Ads.Application.Services.Cache.AdCache.Commands.DeleteAdCacheByAssetId
{
    public class DeleteAdCacheByAssetIdCommand : IRequest<Unit>
    {
        public long AdAssetId { get; set; }

        public DeleteAdCacheByAssetIdCommand(long adAssetId)
        {
            AdAssetId = adAssetId;
        }
    }
}
