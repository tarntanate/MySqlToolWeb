using MediatR;
using System;

namespace Ookbee.Ads.Application.Business.Cache.AdUnitStats.Commands.ArchiveAdUnitStatsById
{
    public class ArchiveAdUnitStatsByIdCommand : IRequest<Unit>
    {
        public DateTimeOffset CaculatedAt { get; set; }
        public long AdUnitId { get; set; }

        public ArchiveAdUnitStatsByIdCommand(DateTimeOffset caculatedAt, long adUnitId)
        {
            CaculatedAt = caculatedAt;
            AdUnitId = adUnitId;
        }
    }
}
