using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using System;
using System.Linq.Expressions;

namespace Ookbee.Ads.Application.Services.Analytics.AdStats
{
    public class AdStatsDto
    {
        public long Id { get; set; }
        public long AdId { get; set; }
        public long Quota { get; set; }
        public long Impression { get; set; }
        public long Click { get; set; }

        public static Expression<Func<AdStatsEntity, AdStatsDto>> Projection
        {
            get
            {
                return entity => new AdStatsDto()
                {
                    Id = entity.Id,
                    AdId = entity.AdId,
                    Quota = entity.Quota,
                    Click = entity.Click,
                    Impression = entity.Impression,
                };
            }
        }
    }
}
