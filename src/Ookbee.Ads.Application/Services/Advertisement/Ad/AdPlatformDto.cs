using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Ookbee.Ads.Application.Services.Advertisement.Ad
{
    public class AdPlatformDto : DefaultDto
    {
        public IEnumerable<AdPlatform> Platforms { get; set; }

        public static Expression<Func<AdEntity, AdPlatformDto>> Projection
        {
            get
            {
                return entity => new AdPlatformDto()
                {
                    Id = entity.Id,
                    Platforms = entity.Platforms
                };
            }
        }
    }
}