using System;
using System.Linq.Expressions;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.Advertiser
{
    public class AdvertiserDto : DefaultDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int TotalCampaign { get; set; }

        public static Expression<Func<AdvertiserEntity, AdvertiserDto>> Projection
        {
            get
            {
                return entity => new AdvertiserDto()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Description = entity.Description,
                    ImagePath = entity.ImagePath,
                    Contact = entity.Contact,
                    Email = entity.Email,
                    PhoneNumber = entity.PhoneNumber,
                    CreatedAt = entity.CreatedAt,
                    UpdatedAt = entity.UpdatedAt,
                    DeletedAt = entity.DeletedAt,
                    TotalCampaign = entity.Campaigns.Count
                };
            }
        }
    }
}
