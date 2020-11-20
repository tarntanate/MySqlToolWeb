using Ookbee.Ads.Common.Builders;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using System;
using System.Linq.Expressions;

namespace Ookbee.Ads.Application.Services.Cache
{
    public static class AdGroupCacheFilter
    {
        public static Expression<Func<AdGroupEntity, bool>> Available()
        {
            return PredicateBuilder
                .True<AdGroupEntity>()
                .And(f => f.DeletedAt == null)
                .And(f => f.Enabled == true);
        }

        public static Expression<Func<AdGroupEntity, bool>> Available(long adGroupId)
        {
            return Available().And(f => f.Id == adGroupId);
        }
    }
}
