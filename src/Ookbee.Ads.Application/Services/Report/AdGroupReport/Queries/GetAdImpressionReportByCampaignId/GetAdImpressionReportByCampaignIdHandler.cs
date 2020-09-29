using MediatR;
using Microsoft.EntityFrameworkCore;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Persistence.EFCore.TimescaleDb;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Report.AdGroupReport.Queries.GetAdImpressionReportByCampaignId
{
    public class GetAdImpressionReportByCampaignIdHandler : IRequestHandler<GetAdImpressionReportByCampaignIdQuery, Response<List<AdImpressionReportByCampaignIdDto>>>
    {
        // private TimescaleDbRepository<AdGroupReportDto> AdGroupReportDbRepo { get; }
        private TimescaleDbContext dbContext { get; }

        public GetAdImpressionReportByCampaignIdHandler(TimescaleDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Response<List<AdImpressionReportByCampaignIdDto>>> Handle(GetAdImpressionReportByCampaignIdQuery request, CancellationToken cancellationToken)
        {
            // var item = new List<AdGroupReportDto>();
            var sqlCommandText = $@"SELECT time_bucket('1 day', ""CreatedAt"" ) AS ""Day"",
                ""CampaignId"", COUNT(*) as ""Total""
                FROM public.""AdImpressionLog""
                WHERE ""CampaignId"" = " + request.CampaignId.ToString() + @"
                GROUP BY ""Day"", ""CampaignId"" 
                ORDER BY ""Day"" ";

            var data = await dbContext.AdImpressionReports.FromSqlRaw(sqlCommandText).Select(AdImpressionReportByCampaignIdDto.Projection).ToListAsync();

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

            var result = new Response<List<AdImpressionReportByCampaignIdDto>>();
            return (data != null)
                ? result.OK(data)
                : result.NotFound();
        }
    }
}
