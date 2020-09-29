using Ookbee.Ads.Common.EntityFrameworkCore.Domain;
using System;

namespace Ookbee.Ads.Domain.Entities.ReportEntities
{
    public class AdImpressionReportEntity : BaseEntity
    {
        public DateTime Day { get; set; }
        public int AdUnitId { get; set; }
        public int AdId { get; set; }
        public long Total { get; set; }
    }
}
