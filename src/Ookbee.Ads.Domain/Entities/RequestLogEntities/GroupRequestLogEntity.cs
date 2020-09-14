using Ookbee.Ads.Common.EntityFrameworkCore.Domain;
using System;

namespace Ookbee.Ads.Domain.Entities.RequestLogEntities
{
    public class GroupRequestLogEntity : BaseEntity, ICreatedAt
    {
        public DateTime? CreatedAt { get; set; }
        public string uuid { get; set; }
        public short AdGroupId { get; set; }
        public short PlatformId { get; set; }
    }
}
