using Ookbee.Ads.Common.Builders;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using System;
using System.Linq.Expressions;

namespace Ookbee.Ads.Application.Services.Cache.AdUnitCache
{
    public static class AdUnitFilter
    {
        public static Expression<Func<AdUnitEntity, bool>> Available()
        {
            return PredicateBuilder
                .True<AdUnitEntity>()
                .And(f => f.DeletedAt == null)
                .And(f => f.AdGroup.Enabled == true)
                .And(f => f.AdGroup.DeletedAt == null);
        }
    }
}
