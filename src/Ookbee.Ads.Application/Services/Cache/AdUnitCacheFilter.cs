using Ookbee.Ads.Common.Builders;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using System;
using System.Linq.Expressions;

namespace Ookbee.Ads.Application.Services.Cache
{
    public static class AdUnitCacheFilter
    {
        public static Expression<Func<AdUnitEntity, bool>> Available()
        {
            return PredicateBuilder
                .True<AdUnitEntity>()
                .And(f => f.DeletedAt == null)
                .And(f => f.AdGroup.Enabled == true)
                .And(f => f.AdGroup.DeletedAt == null);
        }

        public static Expression<Func<AdUnitEntity, bool>> Available(long adUnitId)
        {
            return Available().And(f => f.Id == adUnitId);
        }
    }
}
