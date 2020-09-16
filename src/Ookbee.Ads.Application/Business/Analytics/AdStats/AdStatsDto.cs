using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using System;
using System.Linq.Expressions;

namespace Ookbee.Ads.Application.Business.Analytics.AdStats
{
    public class AdStatsDto
    {
        public long Id { get; set; }
        public long AdId { get; set; }
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
                    Impression = entity.Impression,
                    Click = entity.Click,
                };
            }
        }
    }
}
