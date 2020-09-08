using MediatR;

namespace Ookbee.Ads.Application.Business.Cache.AdAssetCache.Commands.CreateAdAssetCache
{
    public class CreateAdAssetCacheCommand : IRequest<Unit>
    {
        public long AdId { get; set; }

        public CreateAdAssetCacheCommand(long adId)
        {
            AdId = adId;
        }
    }
}
