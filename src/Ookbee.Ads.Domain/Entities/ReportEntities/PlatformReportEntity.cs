using Ookbee.Ads.Common.EntityFrameworkCore.Domain;
using System;

namespace Ookbee.Ads.Domain.Entities.ReportEntities
{
    public class PlatformReportEntity : BaseEntity
    {
        public int PlatformId { get; set; }
        public long Total { get; set; }
    }
}
