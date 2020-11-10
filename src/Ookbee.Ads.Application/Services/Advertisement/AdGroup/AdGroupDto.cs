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

        public static AdGroupDto FromEntity(AdGroupEntity entity)
        {
            return entity == null
                ? null
                : Projection.Compile().Invoke(entity);
        }

        public static Expression<Func<AdGroupEntity, AdGroupDto>> Projection
        {
            get
            {
                return entity => new AdGroupDto()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Description = entity.Description,
                    Placement = entity.Placement,
                    Enabled = entity.Enabled,
                    TotalAdUnit = entity.AdUnits != null ? entity.AdUnits.Where(adunit => adunit.DeletedAt == null).Count() : 0,
                    AdGroupType = AdGroupTypeDto.FromEntity(entity.AdGroupType),
                    Publisher = PublisherDto.FromEntity(entity.Publisher),
                };
            }
        }
    }
}
