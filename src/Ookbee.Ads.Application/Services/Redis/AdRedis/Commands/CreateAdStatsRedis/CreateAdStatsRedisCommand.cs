using MediatR;
using System;

namespace Ookbee.Ads.Application.Services.Redis.AdRedis.Commands.CreateAdStatsRedis
{
    public class CreateAdStatsRedisCommand : IRequest<Unit>
    {
        public DateTimeOffset CaculatedAt { get; set; }
        public long AdId { get; set; }

        public CreateAdStatsRedisCommand(DateTimeOffset caculatedAt, long adId)
        {
            CaculatedAt = caculatedAt;
            AdId = adId;
        }
    }
}
