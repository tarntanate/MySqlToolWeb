using MediatR;

namespace Ookbee.Ads.Application.Services.Redis.AdRedis.Commands.DeleteAdRedis
{
    public class DeleteAdRedisCommand : IRequest<Unit>
    {
        public long AdUnitId { get; private set; }

        public DeleteAdRedisCommand(long adUnitId)
        {
            AdUnitId = adUnitId;
        }
    }
}
