using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using System;
using System.Linq.Expressions;

namespace Ookbee.Ads.Application.Services.Advertisement.Publisher
{
    public class PublisherDto : DefaultDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string CountryCode { get; set; }

        public static PublisherDto FromEntity(PublisherEntity entity)
        {
            return entity == null
                ? null
                : Projection.Compile().Invoke(entity);
        }

        public static Expression<Func<PublisherEntity, PublisherDto>> Projection
        {
            get
            {
                return entity => new PublisherDto()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Description = entity.Description,
                    ImagePath = entity.ImagePath,
                    CountryCode = entity.CountryCode
                };
            }
        }
    }
}
