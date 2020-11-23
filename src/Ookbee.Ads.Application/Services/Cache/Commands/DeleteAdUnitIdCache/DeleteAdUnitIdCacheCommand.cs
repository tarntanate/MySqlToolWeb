using MediatR;

namespace Ookbee.Ads.Application.Services.Cache.Commands.DeleteAdUnitIdCache
{
    public class DeleteAdUnitIdCacheCommand : IRequest<Unit>
    {
        public long AdUnitId { get; set; }

        public DeleteAdUnitIdCacheCommand(long adUnitId)
        {
            AdUnitId = adUnitId;
        }
    }
}
