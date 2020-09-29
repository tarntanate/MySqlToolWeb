using MediatR;
using Microsoft.EntityFrameworkCore;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using Ookbee.Ads.Persistence.EFCore.TimescaleDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Report.AdGroupReport.Queries.GetAdImpressionReportByUnitId
{
    public class GetAdImpressionReportByUnitIdHandler : IRequestHandler<GetAdImpressionReportByUnitIdQuery, Response<List<AdImpressionReportByUnitIdDto>>>
    {
        // private TimescaleDbRepository<AdGroupReportDto> AdGroupReportDbRepo { get; }
        private TimescaleDbContext dbContext { get; }

        public GetAdImpressionReportByUnitIdHandler(TimescaleDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Response<List<AdImpressionReportByUnitIdDto>>> Handle(GetAdImpressionReportByUnitIdQuery request, CancellationToken cancellationToken)
        {
            // var item = new List<AdGroupReportDto>();
            var sqlCommandText = $@"SELECT time_bucket('1 day', ""CreatedAt"" ) AS ""Day"",
                ""UnitId"" AS ""AdUnitId"", COUNT(*) as ""Total""
                FROM public.""AdImpressionLog""
                WHERE ""UnitId"" = " + request.AdUnitId.ToString() + @"
                GROUP BY ""Day"", ""UnitId"" 
                ORDER BY ""Day"" ";

            var data = await dbContext.AdImpressionReports.FromSqlRaw(sqlCommandText).Select(AdImpressionReportByUnitIdDto.Projection).ToListAsync();

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

            var result = new Response<List<AdImpressionReportByUnitIdDto>>();
            return (data != null)
                ? result.OK(data)
                : result.NotFound();
        }
    }
}
