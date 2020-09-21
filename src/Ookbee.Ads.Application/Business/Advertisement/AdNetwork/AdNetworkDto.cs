using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Infrastructure.Models;
using System;
using System.Linq.Expressions;

namespace Ookbee.Ads.Application.Business.Advertisement.AdNetwork
{
    public class AdNetworkDto : DefaultDto
    {
        public Platform Platform { get; set; }
        public string AdNetworkUnitId { get; set; }

        public static Expression<Func<AdNetworkEntity, AdNetworkDto>> Projection
        {
            get
            {
                return entity => new AdNetworkDto()
                {
                    Id = entity.Id,
                    Platform = entity.Platform,
                    AdNetworkUnitId = entity.AdNetworkUnitId,
                };
            }
        }
    }
}