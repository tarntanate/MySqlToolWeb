using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using System;
using System.Linq.Expressions;

namespace Ookbee.Ads.Application.Business.AdGroup
{
    public class AdGroupDto : DefaultDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public static Expression<Func<AdGroupEntity, AdGroupDto>> Projection
        {
            get
            {
                return entity => new AdGroupDto()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Description = entity.Description
                };
            }
        }
    }
}
