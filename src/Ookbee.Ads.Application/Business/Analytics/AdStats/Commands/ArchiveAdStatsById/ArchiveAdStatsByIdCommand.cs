using MediatR;
using System;

namespace Ookbee.Ads.Application.Business.Cache.AdStats.Commands.ArchiveAdStatsById
{
    public class ArchiveAdStatsByIdCommand : IRequest<Unit>
    {
        public DateTimeOffset CaculatedAt { get; set; }
        public long AdId { get; set; }

        public ArchiveAdStatsByIdCommand(DateTimeOffset caculatedAt, long adId)
        {
            CaculatedAt = caculatedAt;
            AdId = adId;
        }
    }
}
