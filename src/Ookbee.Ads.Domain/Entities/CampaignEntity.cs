using System;
using System.Collections.Generic;
using Ookbee.Ads.Common.EntityFrameworkCore.Domain;

namespace Ookbee.Ads.Domain.Entities
{
    public class CampaignEntity : BaseEntity, ICreatedAt, IUpdatedAt, IDeletedAt
    {
        public long AdvertiserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PricingModel { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        
        public virtual AdvertiserEntity Advertiser { get; set; }
        public virtual CampaignCostEntity CampaignCost { get; set; }
        public virtual CampaignImpressionEntity CampaignImpression { get; set; }

        public virtual List<AdEntity> Ads { get; set; }
    }
}
