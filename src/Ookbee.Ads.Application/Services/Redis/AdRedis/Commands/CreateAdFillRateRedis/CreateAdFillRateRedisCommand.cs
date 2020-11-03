using MediatR;
using System;

namespace Ookbee.Ads.Application.Services.Redis.AdRedis.Commands.CreateAdFillRateRedis
{
    public class CreateAdFillRateRedisCommand : IRequest<Unit>
    {
        public DateTimeOffset CaculatedAt { get; set; }
        public long AdUnitId { get; set; }

        public CreateAdFillRateRedisCommand(DateTimeOffset caculatedAt, long adUnitId)
        {
            CaculatedAt = caculatedAt;
            AdUnitId = adUnitId;
        }
    }
}
