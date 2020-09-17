using Ookbee.Ads.Common.EntityFrameworkCore.Domain;
using System;

namespace Ookbee.Ads.Domain.Entities.AdsEntities
{
    public class AdGroupReportEntity : BaseEntity
    {
        public DateTime Day { get; set; }
        public int AdGroupId { get; set; }
        public long Total { get; set; }
    }
}
