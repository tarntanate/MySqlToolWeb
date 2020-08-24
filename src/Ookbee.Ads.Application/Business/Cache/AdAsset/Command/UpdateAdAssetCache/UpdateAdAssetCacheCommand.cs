using MediatR;

namespace Ookbee.Ads.Application.Business.AdAssetCache.Commands.UpdateAdAssetCache
{
    public class UpdateAdAssetCacheCommand : IRequest<Unit>
    {
        public long AdId { get; set; }

        public UpdateAdAssetCacheCommand(long adId)
        {
            AdId = adId;
        }
    }
}
