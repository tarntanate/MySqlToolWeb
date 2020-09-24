using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using System;
using System.Linq.Expressions;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnitType
{
    public class AdUnitTypeDto : DefaultDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public static Expression<Func<AdUnitTypeEntity, AdUnitTypeDto>> Projection
        {
            get
            {
                return entity => new AdUnitTypeDto()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Description = entity.Description
                };
            }
        }
    }
}
