using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using System;
using System.Linq.Expressions;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroupType
{
    public class AdGroupTypeDto : DefaultDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        
        public static AdGroupTypeDto FromEntity(AdGroupTypeEntity entity)
        {
            return entity == null 
                ? null 
                : Projection.Compile().Invoke(entity);
        }

        public static Expression<Func<AdGroupTypeEntity, AdGroupTypeDto>> Projection
        {
            get
            {
                return entity => new AdGroupTypeDto()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Description = entity.Description
                };
            }
        }
    }
}
