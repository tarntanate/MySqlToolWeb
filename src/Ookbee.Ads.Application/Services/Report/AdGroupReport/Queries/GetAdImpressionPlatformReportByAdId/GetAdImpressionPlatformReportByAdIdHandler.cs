using MediatR;
using Microsoft.EntityFrameworkCore;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Persistence.EFCore.TimescaleDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Report.AdGroupReport.Queries.GetAdImpressionPlatformReportByAdId
{
    public class GetAdImpressionPlatformReportByAdIdHandler : IRequestHandler<GetAdImpressionPlatformReportByAdIdQuery, Response<List<PlatformReportDto>>>
    {
        private TimescaleDbContext dbContext { get; }

        public GetAdImpressionPlatformReportByAdIdHandler(TimescaleDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Response<List<PlatformReportDto>>> Handle(GetAdImpressionPlatformReportByAdIdQuery request, CancellationToken cancellationToken)
        {
            var sqlCommandText = $@"SELECT ""PlatformId"" , COUNT(*) as ""Total""
                FROM public.""AdImpressionLog""
                WHERE ""AdId"" = " + request.AdId.ToString() + 
                $@" AND ""CreatedAt"" BETWEEN '{request.StartDate.ToString("yyyy-MM-dd")}' AND '{request.EndDate.AddDays(1).ToString("yyyy-MM-dd")}'
                GROUP BY ""PlatformId"" ";

            var data = await dbContext.PlatformReports.FromSqlRaw(sqlCommandText).Select(PlatformReportDto.Projection).ToListAsync();

            var result = new Response<List<PlatformReportDto>>();
            return (data != null)
                ? result.OK(data)
                : result.NotFound();
        }
    }
}
