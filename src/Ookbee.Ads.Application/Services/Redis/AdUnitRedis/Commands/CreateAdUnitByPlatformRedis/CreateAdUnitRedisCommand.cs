using MediatR;

namespace Ookbee.Ads.Application.Services.Redis.AdUnitRedis.Commands.CreateAdUnitByPlatformRedis
{
    public class CreateAdUnitByPlatformRedisCommand : IRequest<Unit>
    {
        public long AdGroupId { get; private set; }

        public CreateAdUnitByPlatformRedisCommand(long adGroupId)
        {
            AdGroupId = adGroupId;
        }
    }
}
