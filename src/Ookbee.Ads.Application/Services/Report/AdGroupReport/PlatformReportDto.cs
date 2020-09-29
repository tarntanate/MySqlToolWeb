using Ookbee.Ads.Domain.Entities.ReportEntities;
using Ookbee.Ads.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Ookbee.Ads.Application.Business.Report.AdGroupReport
{
    public class PlatformReportDto
    {
        public int PlatformId { get; set; }
        public AdPlatform Name { get; set; }
        public long Total { get; set; }

        public static Expression<Func<PlatformReportEntity, PlatformReportDto>> Projection
        {
            get
            {
                return entity => new PlatformReportDto()
                {
                    PlatformId = entity.PlatformId,
                    Name = (AdPlatform) entity.PlatformId,
                    Total = entity.Total
                };
            }
        }
    } 

}
