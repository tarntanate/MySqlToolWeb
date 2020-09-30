using MediatR;
using System;

namespace Ookbee.Ads.Application.Services.Redis.AdUnitRedis.Commands.CreateAdUnitIdRedis
{
    public class CreateAdUnitIdRedisCommand : IRequest<Unit>
    {
        public DateTimeOffset CaculatedAt { get; private set; }
        public long AdGroupId { get; private set; }

        public CreateAdUnitIdRedisCommand(DateTimeOffset caculatedAt, long adGroupId)
        {
            CaculatedAt = caculatedAt;
            AdGroupId = adGroupId;
        }
    }
}
