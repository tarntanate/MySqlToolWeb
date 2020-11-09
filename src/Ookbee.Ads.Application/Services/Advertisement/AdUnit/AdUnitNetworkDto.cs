using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnit
{
    public class AdUnitNetworkDto
    {
        public string Name { get; set; }
        public IEnumerable<AdUnitNetworkUnitIdDto> AdNetworkUnits { get; set; }

        public static AdUnitNetworkDto FromEntity(AdUnitEntity entity)
        {
            return entity == null
                ? null
                : Projection.Compile().Invoke(entity);
        }

        public static Expression<Func<AdUnitEntity, AdUnitNetworkDto>> Projection
        {
            get
            {
                return entity => new AdUnitNetworkDto()
                {
                    Name = entity.AdNetwork,
                    AdNetworkUnits = entity.AdNetworks.HasValue()
                        ? entity.AdNetworks.AsQueryable().Select(AdUnitNetworkUnitIdDto.Projection).Where(item => item.DeletedAt == null)
                        : null
                };
            }
        }
    }
}