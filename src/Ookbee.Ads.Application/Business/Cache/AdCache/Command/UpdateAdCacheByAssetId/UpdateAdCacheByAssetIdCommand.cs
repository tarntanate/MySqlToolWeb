using MediatR;

namespace Ookbee.Ads.Application.Business.Cache.AdCache.Commands.UpdateAdCacheByAssetId
{
    public class UpdateAdCacheByAssetIdCommand : IRequest<Unit>
    {
        public long AdId { get; set; }

        public UpdateAdCacheByAssetIdCommand(long adId)
        {
            AdId = adId;
        }
    }
}
