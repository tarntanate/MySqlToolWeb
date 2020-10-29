using Ookbee.Ads.Domain.Entities.AdsEntities;
using System;
using System.Linq.Expressions;

namespace Ookbee.Ads.Application.Services.Analytics.AdUnitStats
{
    public class AdUnitStatsDto
    {
        public long Id { get; set; }
        public long AdUnitId { get; set; }
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
                    Request = entity.Request,
                    Fill = entity.Fill,
                };
            }
        }
    }
}
