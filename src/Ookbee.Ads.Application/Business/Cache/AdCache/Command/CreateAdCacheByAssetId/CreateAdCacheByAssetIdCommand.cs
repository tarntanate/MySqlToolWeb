using MediatR;

namespace Ookbee.Ads.Application.Business.Cache.AdCache.Commands.CeateAdCacheByAssetId
{
    public class CreateAdCacheByAssetIdCommand : IRequest<Unit>
    {
        public long AdId { get; set; }

        public CreateAdCacheByAssetIdCommand(long adId)
        {
            AdId = adId;
        }
    }
}
