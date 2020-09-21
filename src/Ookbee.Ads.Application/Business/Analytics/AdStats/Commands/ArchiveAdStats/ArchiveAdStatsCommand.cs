using MediatR;
using System;

namespace Ookbee.Ads.Application.Business.Cache.AdStats.Commands.ArchiveAdStats
{
    public class ArchiveAdStatsCommand : IRequest<Unit>
    {
        public DateTimeOffset CaculatedAt { get; set; }

        public ArchiveAdStatsCommand(DateTimeOffset caculatedAt)
        {
            CaculatedAt = caculatedAt;
        }
    }
}
