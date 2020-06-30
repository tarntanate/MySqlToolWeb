using System;
using System.Collections.Generic;
using Ookbee.Ads.Common.EntityFrameworkCore.Domain;
using Ookbee.Ads.Infrastructure.Enums;

namespace Ookbee.Ads.Domain.Entities.AdsEntities
{
    public class AdEntity : BaseEntity, IBaseIdentity, ICreatedAt, IUpdatedAt, IDeletedAt
    {
        public long Id { get; set; }
        public long AdUnitId { get; set; }
        public long CampaignId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public AdStatus Status { get; set; }
        public int? CountdownSecond { get; set; }
        public string ForegroundColor { get; set; }
        public string BackgroundColor { get; set; }
        public List<string> Analytics { get; set; }
        public List<Platform> Platforms { get; set; }
        public string AppLink { get; set; }
        public string WebLink { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual AdUnitEntity AdUnit { get; set; }
        public virtual CampaignEntity Campaign { get; set; }

        public virtual List<AdAssetEntity> AdAssets { get; set; }
    }
}
