using System;
using System.Collections.Generic;
using Ookbee.Ads.Common.EntityFrameworkCore.Domain;

namespace Ookbee.Ads.Domain.Entities
{
    public class AdEntity : BaseEntity, ICreatedAt, IUpdatedAt, IDeletedAt
    {
        public long AdUnitId { get; set; }
        public long CampaignId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? CountdownSecond { get; set; }
        public string ForegroundColor { get; set; }
        public string BackgroundColor { get; set; }
        public string[] Analytics { get; set; }
        public string[] Platforms { get; set; }
        public string AppLink { get; set; }
        public string WebLink { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }

        public virtual AdUnitEntity AdUnit { get; set; }
        public virtual CampaignEntity Campaign { get; set; }
        
        public virtual List<AdAssetEntity> AdAsset { get; set; }
    }
}
