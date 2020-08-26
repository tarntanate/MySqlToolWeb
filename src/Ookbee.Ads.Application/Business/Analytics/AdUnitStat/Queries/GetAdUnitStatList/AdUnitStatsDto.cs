using System.Linq.Expressions;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using Ookbee.Ads.Infrastructure.Models;
using System;

namespace Ookbee.Ads.Application.Business.Analytics.AdUnitStatsList.Queries.GetAdUnitStatsList
{
    public class AdUnitStatsDto
    {
        public long Id { get; set; }
        public long AdUnitId { get; set; }
        public Platform Platform { get; set; }
        public long Request { get; set; }
        public long Fill { get; set; }

        public static Expression<Func<AdUnitStatsEntity, AdUnitStatsDto>> Projection
        {
            get
            {
                return entity => new AdUnitStatsDto()
                {
                    Id = entity.Id,
                    AdUnitId = entity.AdUnitId,
                    Platform = entity.Platform,
                    Request = entity.Request,
                    Fill = entity.Fill,
                };
            }
        }
    }
}
