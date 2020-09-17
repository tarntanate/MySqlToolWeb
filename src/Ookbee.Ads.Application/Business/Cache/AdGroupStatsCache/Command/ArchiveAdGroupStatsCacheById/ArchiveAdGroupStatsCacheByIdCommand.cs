using MediatR;
using System;

namespace Ookbee.Ads.Application.Business.Cache.AdGroupStatsCache.Commands.ArchiveAdGroupStatsCacheById
{
    public class ArchiveAdGroupStatsCacheByIdCommand : IRequest<Unit>
    {
        public DateTime CaculatedAt { get; set; }
        public long AdGroupId { get; set; }

        public ArchiveAdGroupStatsCacheByIdCommand(DateTime caculatedAt, long adGroupId)
        {
            CaculatedAt = caculatedAt;
            AdGroupId = adGroupId;
        }
    }
}
