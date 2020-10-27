using MediatR;
using System;

namespace Ookbee.Ads.Application.Services.Redis.AdRedis.Commands.DeleteAdRedis
{
    public class DeleteAdRedisCommand : IRequest<Unit>
    {
        public DateTimeOffset CaculatedAt { get; private set; }
        public long AdUnitId { get; private set; }

        public DeleteAdRedisCommand(DateTimeOffset caculatedAt, long adUnitId)
        {
            CaculatedAt = caculatedAt;
            AdUnitId = adUnitId;
        }
    }
}
