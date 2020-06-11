using System;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Ookbee.Ads.Application.Business.Advertiser;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Domain.Entities;

namespace Ookbee.Ads.Application.Business.Campaign
{
    public class CampaignDto : DefaultDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public string PricingModel { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Budget { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public decimal? CostPerUnit { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Quota { get; set; }
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
                    StartDate = entity.StartDate,
                    EndDate = entity.EndDate,
                    PricingModel = entity.PricingModel,
                    Budget = entity.CampaignCost.Budget,
                    CostPerUnit = entity.CampaignCost.CostPerUnit,
                    Quota = entity.CampaignImpression.Quota,
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