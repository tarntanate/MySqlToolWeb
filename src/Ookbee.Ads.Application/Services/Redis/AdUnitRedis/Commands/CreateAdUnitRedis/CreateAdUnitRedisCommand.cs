using MediatR;
using System;

namespace Ookbee.Ads.Application.Services.Redis.AdUnitRedis.Commands.CreateAdUnitRedis
{
    public class CreateAdUnitRedisCommand : IRequest<Unit>
    {
        public DateTimeOffset CaculatedAt { get; private set; }
        public long AdGroupId { get; private set; }

        public CreateAdUnitRedisCommand(DateTimeOffset caculatedAt, long adGroupId)
        {
            CaculatedAt = caculatedAt;
            AdGroupId = adGroupId;
        }
    }
}
