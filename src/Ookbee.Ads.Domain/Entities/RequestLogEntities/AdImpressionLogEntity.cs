using Ookbee.Ads.Common.EntityFrameworkCore.Domain;
using System;

namespace Ookbee.Ads.Domain.Entities.RequestLogEntities
{
    public class AdImpressionLogEntity : BaseEntity
    {
        public DateTime? CreatedAt { get; set; }
        public string uuid { get; set; }
        public int AdId { get; set; }
        public int CampaignId { get; set; }
        public int UnitId { get; set; }
        public short PlatformId { get; set; }
    }
}
