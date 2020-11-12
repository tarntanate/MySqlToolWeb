using MediatR;
using Microsoft.EntityFrameworkCore;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Persistence.EFCore.TimescaleDb;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Report.AdGroupReport.Queries.GetAdImpressionReportByPublisherId
{
    public class GetAdImpressionReportByPublisherIdHandler : IRequestHandler<GetAdImpressionReportByPublisherIdQuery, Response<List<AdImpressionReportDto>>>
    {
        private TimescaleDbContext dbContext { get; }

        public GetAdImpressionReportByPublisherIdHandler(TimescaleDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Response<List<AdImpressionReportDto>>> Handle(GetAdImpressionReportByPublisherIdQuery request, CancellationToken cancellationToken)
        {
            // var item = new List<AdGroupReportDto>();
            var sqlCommandText = $@"SELECT time_bucket('1 day', ""CreatedAt"" ) AS ""Day"", COUNT(*) as ""Total""
                FROM public.""AdImpressionLog""
                WHERE ""PublisherId"" = " + request.PublisherId.ToString() +
                $@" AND ""CreatedAt"" BETWEEN '{request.StartDate.ToString("yyyy-MM-dd")}' AND '{request.EndDate.AddDays(1).ToString("yyyy-MM-dd")}'
                GROUP BY ""Day""
                ORDER BY ""Day"" ";

            var data = await dbContext.AdImpressionReports.FromSqlRaw(sqlCommandText).Select(AdImpressionReportDto.Projection).ToListAsync();

            var result = new Response<List<AdImpressionReportDto>>();
            return (data != null)
                ? result.OK(data)
                : result.NotFound();
        }
    }
}
