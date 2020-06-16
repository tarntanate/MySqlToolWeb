using System;
using Ookbee.Ads.Common.EntityFrameworkCore.Domain;

namespace Ookbee.Ads.Domain.Entities.AdsEntities
{
    public class AdAssetEntity : BaseEntity, ICreatedAt, IUpdatedAt, IDeletedAt
    {
        public long AdId { get; set; }
        public string AssetType { get; set; }
        public string AssetPath { get; set; }
        public string Position { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual AdEntity Ad { get; set; }
    }
}
