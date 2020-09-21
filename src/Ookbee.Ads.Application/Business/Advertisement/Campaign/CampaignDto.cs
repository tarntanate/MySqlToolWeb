using Ookbee.Ads.Application.Business.Advertisement.Advertiser;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Ookbee.Ads.Application.Business.Advertisement.Campaign
{
    public class CampaignDto : DefaultDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int TotalAds { get; set; }
        public AdvertiserDto Advertiser { get; set; }

        public static Expression<Func<CampaignEntity, CampaignDto>> Projection
        {
            get
            {
                return entity => new CampaignDto()
                {
                    Id = entity.Id,
                    CreatedAt = entity.CreatedAt,
                    UpdatedAt = entity.UpdatedAt,
                    DeletedAt = entity.DeletedAt,
                    Name = entity.Name,
                    Description = entity.Description,
                    TotalAds = entity.Ads.Where(ad => ad.DeletedAt == null).Count(),
                    Advertiser = new AdvertiserDto()
                    {
                        Id = entity.Advertiser.Id,
                        Name = entity.Advertiser.Name,
                        Description = entity.Advertiser.Description,
                        ImagePath = entity.Advertiser.ImagePath,
                        Contact = entity.Advertiser.Contact,
                        Email = entity.Advertiser.Email,
                        PhoneNumber = entity.Advertiser.PhoneNumber,
                    }
                };
            }
        }
    }
}