using Ookbee.Ads.Application.Services.Advertisement.AdUnitType;
using Ookbee.Ads.Application.Services.Advertisement.Publisher;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using System;
using System.Linq.Expressions;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroup
{
    public class AdGroupDto : DefaultDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public AdUnitTypeDto AdUnitType { get; set; }
        public PublisherDto Publisher { get; set; }

        public static Expression<Func<AdGroupEntity, AdGroupDto>> Projection
        {
            get
            {
                return entity => new AdGroupDto()
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
