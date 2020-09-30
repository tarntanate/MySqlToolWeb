using MediatR;

namespace Ookbee.Ads.Application.Services.Redis.AdUnitRedis.Commands.DeleteAdUnitRedis
{
    public class DeleteAdUnitRedisCommand : IRequest<Unit>
    {
        public long AdGroupId { get; private set; }
        
        public DeleteAdUnitRedisCommand(long adGroupId)
        {
            AdGroupId = adGroupId;
        }
    }
}
