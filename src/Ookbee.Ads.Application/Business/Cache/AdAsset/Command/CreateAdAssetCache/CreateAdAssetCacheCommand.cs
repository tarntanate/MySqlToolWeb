using MediatR;

namespace Ookbee.Ads.Application.Business.AdAssetCache.Commands.CreateAdAssetCache
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
