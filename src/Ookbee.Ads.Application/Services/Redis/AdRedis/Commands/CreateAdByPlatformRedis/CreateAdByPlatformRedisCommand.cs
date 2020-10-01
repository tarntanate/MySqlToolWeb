using MediatR;

namespace Ookbee.Ads.Application.Services.Redis.AdRedis.Commands.CreateAdByPlatformRedis
{
    public class CreateAdByPlatformRedisCommand : IRequest<Unit>
    {
        public long AdId { get; private set; }

        public CreateAdByPlatformRedisCommand(long adId)
        {
            AdId = adId;
        }
    }
}
