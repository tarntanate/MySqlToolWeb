using MediatR;
using System;

namespace Ookbee.Ads.Application.Business.Cache.AdGroupStats.Commands.ArchiveAdGroupStatsById
{
    public class ArchiveAdGroupStatsByIdCommand : IRequest<Unit>
    {
        public DateTimeOffset CaculatedAt { get; set; }
        public long AdGroupId { get; set; }

        public ArchiveAdGroupStatsByIdCommand(DateTimeOffset caculatedAt, long adGroupId)
        {
            CaculatedAt = caculatedAt;
            AdGroupId = adGroupId;
        }
    }
}
