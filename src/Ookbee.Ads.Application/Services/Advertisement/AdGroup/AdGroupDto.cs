using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Advertisement.AdGroupType;
using Ookbee.Ads.Application.Services.Advertisement.Publisher;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroup
{
    public class AdGroupDto : DefaultDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Placement { get; set; }
        public bool Enabled { get; set; }
        public int TotalAdUnit { get; set; }
        public AdGroupTypeDto AdGroupType { get; set; }
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
                    Enabled = entity.Enabled,
                    Placement = entity.Placement,
                    TotalAdUnit = entity.AdUnits.Where(adunit => adunit.DeletedAt == null).Count(),
                    AdGroupType = new AdGroupTypeDto()
                    {
                        Id = entity.AdGroupType.Id,
                        Name = entity.AdGroupType.Name,
                        Description = entity.AdGroupType.Description
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
