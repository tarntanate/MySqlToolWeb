using MediatR;
using Microsoft.EntityFrameworkCore;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using Ookbee.Ads.Persistence.EFCore.TimescaleDb;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Report.AdGroupReport.Queries.GetAdGroupReportByGroupId
{
    public class GetAdGroupReportByGroupIdHandler : IRequestHandler<GetAdGroupReportByGroupIdQuery, HttpResult<List<AdGroupReportDto>>>
    {
        // private TimescaleDbRepository<AdGroupReportDto> AdGroupReportDbRepo { get; }
        private TimescaleDbContext dbContext { get; }

        public GetAdGroupReportByGroupIdHandler(TimescaleDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<HttpResult<List<AdGroupReportDto>>> Handle(GetAdGroupReportByGroupIdQuery request, CancellationToken cancellationToken)
        {
            var item = new List<AdGroupReportDto>();

            // Execute a query.
            using (var dr = await dbContext.Database.ExecuteSqlQueryAsync($@"SELECT time_bucket('1 day', ""CreatedAt"" ) AS ""Day""
        ""AdGroupId"" , COUNT(*) as ""Total""
        FROM public.""GroupRequestLog""
        GROUP BY ""Day"", ""AdGroupId"" 
        ORDER BY ""Day"" DESC"))
            {
                // Output rows.
                var reader = dr.DbDataReader;
                while (reader.Read())
                {
                    item.Add(new AdGroupReportDto()
                    {
                        Day = (DateTime)reader[0],
                        AdGroupId = (int)reader[1],
                        Total = (long)reader[2]
                    });
                }
            }

            var result = new HttpResult<List<AdGroupReportDto>>();
            return (item != null)
                ? result.Success(item)
                : result.Fail(404, $"Fail");
        }
    }
}
