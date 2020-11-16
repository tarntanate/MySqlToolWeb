using MediatR;
using System;

namespace Ookbee.Ads.Application.Services.Redis.AdRedis.Commands.ArchiveAdStatsByIdRedis
{
    public class ArchiveAdStatsByIdRedisCommand : IRequest<Unit>
    {
        public DateTimeOffset CaculatedAt { get; private set; }
        public long AdId { get; private set; }

        public ArchiveAdStatsByIdRedisCommand(DateTimeOffset caculatedAt, long adId)
        {
            CaculatedAt = caculatedAt;
            AdId = adId;
        }
    }
}
