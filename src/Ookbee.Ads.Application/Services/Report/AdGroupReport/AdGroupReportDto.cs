using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using Ookbee.Ads.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Ookbee.Ads.Application.Business.Report.AdGroupReport
{
    public class AdGroupReportDto
    {
        public DateTime Day { get; set; }
        public int AdGroupId { get; set; }
        public long Total { get; set; }

        public static Expression<Func<AdGroupReportEntity, AdGroupReportDto>> Projection
        {
            get
            {
                return entity => new AdGroupReportDto()
                {
                    Day = entity.Day,
                    AdGroupId = entity.AdGroupId,
                    Total = entity.Total
                };
            }
        }
    }

    public class AdGroupSummaryReportDto
    {
        public DateTime Day { get; set; }
        public long Total { get; set; }

        public static Expression<Func<AdGroupReportEntity, AdGroupSummaryReportDto>> Projection
        {
            get
            {
                return entity => new AdGroupSummaryReportDto()
                {
                    Day = entity.Day.Date,
                    Total = entity.Total
                };
            }
        }
    }

}
