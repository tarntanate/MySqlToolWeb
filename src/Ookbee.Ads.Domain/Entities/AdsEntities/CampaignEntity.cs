using System;
using System.Collections.Generic;
using Ookbee.Ads.Common.EntityFrameworkCore.Domain;
using Ookbee.Ads.Infrastructure.Enums;

namespace Ookbee.Ads.Domain.Entities.AdsEntities
{
    public class CampaignEntity : BaseEntity, ICreatedAt, IUpdatedAt, IDeletedAt
    {
        public long AdvertiserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public PricingModel PricingModel { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual AdvertiserEntity Advertiser { get; set; }
        public virtual CampaignCostEntity CampaignCost { get; set; }
        public virtual CampaignImpressionEntity CampaignImpression { get; set; }

        public virtual List<AdEntity> Ads { get; set; }
    }
}
