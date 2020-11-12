using Ookbee.Ads.Common.EntityFrameworkCore.Domain;
using Ookbee.Ads.Infrastructure.Models;
using System;

namespace Ookbee.Ads.Domain.Entities.AdsEntities
{
    public class AdAssetEntity : BaseEntity, IBaseIdentity, ICreatedAt, IUpdatedAt, IDeletedAt
    {
        public long Id { get; set; }
        public long AdId { get; set; }
        public string AssetType { get; set; }
        public string AssetPath { get; set; }
        public AdPosition Position { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }

        public virtual AdEntity Ad { get; set; }
    }
}
