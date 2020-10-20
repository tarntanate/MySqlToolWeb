using MediatR;
using Microsoft.EntityFrameworkCore;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.ReportEntities;
using Ookbee.Ads.Persistence.EFCore.TimescaleDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Report.AdGroupReport.Queries.GetAdGroupReportByGroupId
{
    public class GetAdGroupReportByGroupIdHandler : IRequestHandler<GetAdGroupReportByGroupIdQuery, Response<List<AdSummaryReportDto>>>
    {
        // private TimescaleDbRepository<AdGroupReportDto> AdGroupReportDbRepo { get; }
        private TimescaleDbContext dbContext { get; }

        public GetAdGroupReportByGroupIdHandler(TimescaleDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Response<List<AdSummaryReportDto>>> Handle(GetAdGroupReportByGroupIdQuery request, CancellationToken cancellationToken)
        {
            // var item = new List<AdGroupReportDto>();
            var sqlCommandText = $@"SELECT time_bucket('1 day', ""CreatedAt"" ) AS ""Day"",
                COUNT(*) as ""Total""
                FROM public.""GroupRequestLog""
                WHERE ""AdGroupId"" = " + request.AdGroupId.ToString() + 
                $@" AND ""CreatedAt"" BETWEEN '{request.StartDate.ToString("yyyy-MM-dd")}' AND '{request.EndDate.AddDays(1).ToString("yyyy-MM-dd")}'
                GROUP BY ""Day""
                ORDER BY ""Day"" ";

            var data = await dbContext.AdGroupReports.FromSqlRaw(sqlCommandText).Select(AdSummaryReportDto.Projection).ToListAsync();

            // Execute a query.
            // using (var dr = await dbContext.Database.ExecuteSqlQueryAsync()
            // {
            //     // Output rows.
            //     var reader = dr.DbDataReader;
            //     while (reader.Read())
            //     {
            //         item.Add(new AdGroupReportDto()
            //         {
            //             Day = (DateTime)reader[0],
            //             AdGroupId = (int)reader[1],
            //             Total = (long)reader[2]
            //         });
            //     }
            // }

            var result = new Response<List<AdSummaryReportDto>>();
            return (data != null)
                ? result.OK(data)
                : result.NotFound();
        }
    }
}
