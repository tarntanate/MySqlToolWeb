using MediatR;

namespace Ookbee.Ads.Application.Business.Cache.AdCache.Commands.DeleteAdCacheByAssetId
{
    public class DeleteAdCacheByAssetIdCommand : IRequest<Unit>
    {
        public long AdId { get; set; }

        public DeleteAdCacheByAssetIdCommand(long adId)
        {
            AdId = adId;
        }
    }
}
