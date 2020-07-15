using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Infrastructure.Enums;

namespace Ookbee.Ads.Application.Business.Banner
{
    public class AdUnitDto : DefaultDto
    {
        public string Name { get; set; }
        public AdUnitTypeEntity AdUnitType { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, ReferenceLoopHandling = ReferenceLoopHandling.Ignore)]
        public IEnumerable<AdNetwork> AdNetworks { get; set; }

        public static Expression<Func<AdUnitEntity, AdUnitDto>> Projection
        {
            get
            {
                return entity => new AdUnitDto()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    AdUnitType = entity.AdUnitType,
                    AdNetworks = entity.AdNetworks
                };
            }
        }
     
    }
}