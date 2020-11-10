using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Infrastructure.Models;
using System;
using System.Linq.Expressions;

namespace Ookbee.Ads.Application.Services.Advertisement.AdNetwork
{
    public class AdNetworkDto : DefaultDto
    {
        public AdPlatform Platform { get; set; }
        public long AdUnitId { get; set; }
        public string AdNetwork { get; set; }
        public string AdNetworkUnitId { get; set; }

        public static AdNetworkDto FromEntity(AdNetworkEntity entity)
        {
            return entity == null
                ? null
                : Projection.Compile().Invoke(entity);
        }

        public static Expression<Func<AdNetworkEntity, AdNetworkDto>> Projection
        {
            get
            {
                return entity => new AdNetworkDto()
                {
                    Id = entity.Id,
                    Platform = entity.Platform,
                    AdUnitId = entity.AdUnit.Id,
                    AdNetwork = entity.AdUnit.AdNetwork,
                    AdNetworkUnitId = entity.AdNetworkUnitId,
                };
            }
        }
    }
}
