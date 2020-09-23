using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using System;
using System.Linq.Expressions;

namespace Ookbee.Ads.Application.Services.Analytics.AdGroupStat
{
    public class AdGroupStatsDto
    {
        public long AdGroupId { get; set; }
        public long Request { get; set; }
        public DateTimeOffset CaculatedAt { get; set; }

        public static Expression<Func<AdGroupStatsEntity, AdGroupStatsDto>> Projection
        {
            get
            {
                return entity => new AdGroupStatsDto()
                {
                    AdGroupId = entity.AdGroupId,
                    Request = entity.Request,
                    CaculatedAt = entity.CaculatedAt,
                };
            }
        }
    }
}
