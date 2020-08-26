using System.Linq.Expressions;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using Ookbee.Ads.Infrastructure.Models;
using System;

namespace Ookbee.Ads.Application.Business.Analytics.AdStatsList.Queries.GetAdStatsList
{
    public class AdStatsDto
    {
        public long Id { get; set; }
        public long AdId { get; set; }
        public Platform Platform { get; set; }
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
                    Platform = entity.Platform,
                    Impression = entity.Impression,
                    Click = entity.Click,
                };
            }
        }
    }
}
