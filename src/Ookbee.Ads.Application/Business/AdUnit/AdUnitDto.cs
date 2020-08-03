using Newtonsoft.Json;
using Ookbee.Ads.Application.Business.AdUnitType;
using Ookbee.Ads.Application.Business.Publisher;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Ookbee.Ads.Application.Business.AdUnit
{
    public class AdUnitDto : DefaultDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public AdUnitTypeDto AdUnitType { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public PublisherDto Publisher { get; set; }

        public static Expression<Func<AdUnitEntity, AdUnitDto>> Projection
        {
            get
            {
                return entity => new AdUnitDto()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Description = entity.Description,
                    AdUnitType = new AdUnitTypeDto()
                    {
                        Id = entity.AdUnitType.Id,
                        Name = entity.AdUnitType.Name,
                        Description = entity.AdUnitType.Description
                    },
                    Publisher = new PublisherDto()
                    {
                        Id = entity.Publisher.Id,
                        Name = entity.Publisher.Name,
                        Description = entity.Publisher.Description
                    }
                };
            }
        }
    }
}