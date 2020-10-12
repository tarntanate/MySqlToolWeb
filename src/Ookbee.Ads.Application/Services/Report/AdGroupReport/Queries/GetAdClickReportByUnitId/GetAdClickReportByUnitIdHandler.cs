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

namespace Ookbee.Ads.Application.Business.Report.AdGroupReport.Queries.GetAdClickReportByUnitId
{
    public class GetAdClickReportByUnitIdHandler : IRequestHandler<GetAdClickReportByUnitIdQuery, Response<List<AdReportByUnitIdDto>>>
    {
        private TimescaleDbContext dbContext { get; }

        public GetAdClickReportByUnitIdHandler(TimescaleDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Response<List<AdReportByUnitIdDto>>> Handle(GetAdClickReportByUnitIdQuery request, CancellationToken cancellationToken)
        {
            var sqlCommandText = $@"SELECT time_bucket('1 day', ""CreatedAt"" ) AS ""Day"",
                ""UnitId"" AS ""AdUnitId"", COUNT(*) as ""Total""
                FROM public.""AdClickLog""
                WHERE ""UnitId"" = " + request.UnitId.ToString() + @"
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
