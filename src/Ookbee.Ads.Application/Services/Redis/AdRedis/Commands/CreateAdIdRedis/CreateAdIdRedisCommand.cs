using MediatR;

namespace Ookbee.Ads.Application.Services.Redis.AdRedis.Commands.CreateAdIdRedis
{
    public class CreateAdIdRedisCommand : IRequest<Unit>
    {
        public long AdUnitId { get; private set; }

        public CreateAdIdRedisCommand(long adUnitId)
        {
            AdUnitId = adUnitId;
        }
    }
}
