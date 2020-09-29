using Ookbee.Ads.Domain.Entities.ReportEntities;
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

    public class AdUnitImpressionReportDto
    {
        public DateTime Day { get; set; }
        public int AdUnitId { get; set; }
        public long Total { get; set; }

        public static Expression<Func<AdImpressionReportEntity, AdUnitImpressionReportDto>> Projection
        {
            get
            {
                return entity => new AdUnitImpressionReportDto()
                {
                    Day = entity.Day,
                    AdUnitId = entity.AdUnitId,
                    Total = entity.Total
                };
            }
        }
    }

     public class AdImpressionReportDto
    {
        public DateTime Day { get; set; }
        public int AdId { get; set; }
        public long Total { get; set; }

        public static Expression<Func<AdImpressionReportEntity, AdImpressionReportDto>> Projection
        {
            get
            {
                return entity => new AdImpressionReportDto()
                {
                    Day = entity.Day,
                    AdId = entity.AdUnitId,
                    Total = entity.Total
                };
            }
        }
    }

    public class AdSummaryReportDto
    {
        public DateTime Day { get; set; }
        public long Total { get; set; }

        public static Expression<Func<AdGroupReportEntity, AdSummaryReportDto>> Projection
        {
            get
            {
                return entity => new AdSummaryReportDto()
                {
                    Day = entity.Day.Date,
                    Total = entity.Total
                };
            }
        }
    }

}
