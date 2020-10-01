using MediatR;
using System;

namespace Ookbee.Ads.Application.Services.Redis.AdUnitRedis.Commands.ArchiveAdUnitStatsRedis
{
    public class ArchiveAdUnitStatsRedisCommand : IRequest<Unit>
    {
        public DateTimeOffset CaculatedAt { get; private set; }

        public ArchiveAdUnitStatsRedisCommand(DateTimeOffset caculatedAt)
        {
            CaculatedAt = caculatedAt;
        }
    }
}
