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

namespace Ookbee.Ads.Application.Business.Report.AdGroupReport.Queries.GetAdGroupPlatformReportByGroupId
{
    public class GetAdGroupPlatformReportByGroupIdHandler : IRequestHandler<GetAdGroupPlatformReportByGroupIdQuery, Response<List<PlatformReportDto>>>
    {
        private TimescaleDbContext dbContext { get; }

        public GetAdGroupPlatformReportByGroupIdHandler(TimescaleDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Response<List<PlatformReportDto>>> Handle(GetAdGroupPlatformReportByGroupIdQuery request, CancellationToken cancellationToken)
        {
            var sqlCommandText = $@"SELECT ""PlatformId"" , COUNT(*) as ""Total""
                FROM public.""GroupRequestLog""
                WHERE ""AdGroupId"" = " + request.AdGroupId.ToString() + @"
                GROUP BY ""PlatformId"" ";

            var data = await dbContext.PlatformReports.FromSqlRaw(sqlCommandText).Select(PlatformReportDto.Projection).ToListAsync();

            var result = new Response<List<PlatformReportDto>>();
            return (data != null)
                ? result.Success(data)
                : result.Fail(404, $"Fail");
        }
    }
}
