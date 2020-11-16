using MediatR;
using System;

namespace Ookbee.Ads.Application.Services.Redis.AdUnitRedis.Commands.DeleteAdUnitRedis
{
    public class DeleteAdUnitRedisCommand : IRequest<Unit>
    {
        public DateTimeOffset CaculatedAt { get; private set; }
        public long AdGroupId { get; private set; }

        public DeleteAdUnitRedisCommand(DateTimeOffset caculatedAt, long adGroupId)
        {
            CaculatedAt = caculatedAt;
            AdGroupId = adGroupId;
        }
    }
}
