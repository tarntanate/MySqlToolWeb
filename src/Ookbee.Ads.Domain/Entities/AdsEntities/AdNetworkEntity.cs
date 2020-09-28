using Ookbee.Ads.Common.EntityFrameworkCore.Domain;
using Ookbee.Ads.Infrastructure.Models;
using System;

namespace Ookbee.Ads.Domain.Entities.AdsEntities
{
    public class AdNetworkEntity : BaseEntity, IBaseIdentity, ICreatedAt, IUpdatedAt, IDeletedAt
    {
        public long Id { get; set; }
        public long AdUnitId { get; set; }
        public string AdNetworkUnitId { get; set; }
        public AdPlatform Platform { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }

        public virtual AdUnitEntity AdUnit { get; set; }
    }
}
