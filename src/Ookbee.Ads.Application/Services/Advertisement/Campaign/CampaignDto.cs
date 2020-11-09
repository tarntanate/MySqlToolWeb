using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Advertisement.Advertiser;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Ookbee.Ads.Application.Services.Advertisement.Campaign
{
    public class CampaignDto : DefaultDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int TotalAds { get; set; }
        public AdvertiserDto Advertiser { get; set; }

        public static CampaignDto FromEntity(CampaignEntity entity)
        {
            return entity == null
                ? null
                : Projection.Compile().Invoke(entity);
        }

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
                    Advertiser = AdvertiserDto.FromEntity(entity.Advertiser)
                };
            }
        }
    }
}