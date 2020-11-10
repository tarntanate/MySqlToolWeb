using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Infrastructure.Models;
using System;
using System.Linq.Expressions;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnit
{
    public class AdUnitNetworkUnitIdDto : DefaultDto
    {
        public AdPlatform Platform { get; set; }
        public string AdNetworkUnitId { get; set; }

        public static AdUnitNetworkUnitIdDto FromEntity(AdNetworkEntity entity)
        {
            return entity == null
                ? null
                : Projection.Compile().Invoke(entity);
        }

        public static Expression<Func<AdNetworkEntity, AdUnitNetworkUnitIdDto>> Projection
        {
            get
            {
                return entity => new AdUnitNetworkUnitIdDto()
                {
                    Id = entity.Id,
                    Platform = entity.Platform,
                    AdNetworkUnitId = entity.AdNetworkUnitId
                };
            }
        }
    }
}