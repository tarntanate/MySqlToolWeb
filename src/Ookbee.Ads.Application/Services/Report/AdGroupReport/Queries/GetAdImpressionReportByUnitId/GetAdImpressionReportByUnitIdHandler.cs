﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Persistence.EFCore.TimescaleDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Report.AdGroupReport.Queries.GetAdImpressionReportByUnitId
{
    public class GetAdImpressionReportByUnitIdHandler : IRequestHandler<GetAdImpressionReportByUnitIdQuery, Response<List<AdReportByUnitIdDto>>>
    {
        // private TimescaleDbRepository<AdGroupReportDto> AdGroupReportDbRepo { get; }
        private TimescaleDbContext dbContext { get; }

        public GetAdImpressionReportByUnitIdHandler(TimescaleDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Response<List<AdReportByUnitIdDto>>> Handle(GetAdImpressionReportByUnitIdQuery request, CancellationToken cancellationToken)
        {
            // var item = new List<AdGroupReportDto>();
            var sqlCommandText = $@"SELECT time_bucket('1 day', ""CreatedAt"" ) AS ""Day"",
                ""UnitId"" AS ""AdUnitId"", COUNT(*) as ""Total""
                FROM public.""AdImpressionLog""
                WHERE ""UnitId"" = " + request.AdUnitId.ToString() +
                $@" AND ""CreatedAt"" BETWEEN '{request.StartDate.ToString("yyyy-MM-dd")}' AND '{request.EndDate.AddDays(1).ToString("yyyy-MM-dd")}'
                GROUP BY ""Day"", ""UnitId"" 
                ORDER BY ""Day"" ";

            var data = await dbContext.AdImpressionReports.FromSqlRaw(sqlCommandText).Select(AdReportByUnitIdDto.Projection).ToListAsync();

            var result = new Response<List<AdReportByUnitIdDto>>();
            return (data != null)
                ? result.OK(data)
                : result.NotFound();
        }
    }
}