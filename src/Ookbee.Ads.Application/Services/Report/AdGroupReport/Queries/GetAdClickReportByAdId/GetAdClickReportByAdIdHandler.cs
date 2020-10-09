﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using Ookbee.Ads.Persistence.EFCore.TimescaleDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Report.AdGroupReport.Queries.GetAdClickReportByAdId
{
    public class GetAdClickReportByAdIdHandler : IRequestHandler<GetAdClickReportByAdIdQuery, Response<List<AdImpressionReportByAdIdDto>>>
    {
        // private TimescaleDbRepository<AdGroupReportDto> AdGroupReportDbRepo { get; }
        private TimescaleDbContext dbContext { get; }

        public GetAdClickReportByAdIdHandler(TimescaleDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Response<List<AdImpressionReportByAdIdDto>>> Handle(GetAdClickReportByAdIdQuery request, CancellationToken cancellationToken)
        {
            // var item = new List<AdGroupReportDto>();
            var sqlCommandText = $@"SELECT time_bucket('1 day', ""CreatedAt"" ) AS ""Day"",
                ""AdId"", COUNT(*) as ""Total""
                FROM public.""AdClickLog""
                WHERE ""AdId"" = " + request.AdId.ToString() + @"
                GROUP BY ""Day"", ""AdId"" 
                ORDER BY ""Day"" ";

            var data = await dbContext.AdImpressionReports.FromSqlRaw(sqlCommandText).Select(AdImpressionReportByAdIdDto.Projection).ToListAsync();

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

            var result = new Response<List<AdImpressionReportByAdIdDto>>();
            return (data != null)
                ? result.OK(data)
                : result.NotFound();
        }
    }
}
