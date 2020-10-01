using MediatR;
using System;

namespace Ookbee.Ads.Application.Services.Redis.AdGroupRedis.Commands.ArchiveAdGroupStatsRedis
{
    public class ArchiveAdGroupStatsRedisCommand : IRequest<Unit>
    {
        public DateTimeOffset CaculatedAt { get; private set; }

        public ArchiveAdGroupStatsRedisCommand(DateTimeOffset caculatedAt)
        {
            CaculatedAt = caculatedAt;
        }
    }
}
