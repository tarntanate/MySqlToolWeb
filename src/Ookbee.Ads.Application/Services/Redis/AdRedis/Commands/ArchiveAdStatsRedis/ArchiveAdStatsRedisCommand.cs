using MediatR;
using System;

namespace Ookbee.Ads.Application.Services.Redis.AdRedis.Commands.ArchiveAdStatsRedis
{
    public class ArchiveAdStatsRedisCommand : IRequest<Unit>
    {
        public DateTimeOffset CaculatedAt { get; private set; }

        public ArchiveAdStatsRedisCommand(DateTimeOffset caculatedAt)
        {
            CaculatedAt = caculatedAt;
        }
    }
}
