using System;
using System.ComponentModel.DataAnnotations.Schema;
using Ookbee.Ads.Common.EntityFrameworkCore.Domain;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Domain.Entities.AdsEntities
{
    public class ActivityLogEntity : BaseEntity, IBaseIdentity, ICreatedAt
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long ObjectId { get; set; }
        public string ObjectType { get; set; }
        [Column(TypeName = "jsonb")]
        public string ObjectData { get; set; }
        public LogEvent Activity { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual UserEntity User { get; set; }
    }
}
