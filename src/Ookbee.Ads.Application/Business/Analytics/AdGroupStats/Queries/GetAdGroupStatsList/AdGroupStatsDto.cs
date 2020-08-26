using System.Linq.Expressions;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using Ookbee.Ads.Infrastructure.Models;
using System;

namespace Ookbee.Ads.Application.Business.Analytics.AdGroupStatsList.Queries.GetAdGroupStatsList
{
    public class AdGroupStatsDto
    {
        public long Id { get; set; }
        public long AdGroupId { get; set; }
        public Platform Platform { get; set; }
        public long Request { get; set; }

        public static Expression<Func<AdGroupStatsEntity, AdGroupStatsDto>> Projection
        {
            get
            {
                return entity => new AdGroupStatsDto()
                {
                    Id = entity.Id,
                    AdGroupId = entity.AdGroupId,
                    Platform = entity.Platform,
                    Request = entity.Request,
                };
            }
        }
    }
}
