using MediatR;

namespace Ookbee.Ads.Application.Services.Redis.AdRedis.Commands.CreateAdIdRedis
{
    public class CreateAdIdRedisCommand : IRequest<Unit>
    {
        public long AdId { get; private set; }

        public CreateAdIdRedisCommand(long adId)
        {
            AdId = adId;
        }
    }
}
