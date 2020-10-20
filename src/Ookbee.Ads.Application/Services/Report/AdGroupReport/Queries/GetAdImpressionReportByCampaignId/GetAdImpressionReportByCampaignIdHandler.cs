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
                WHERE ""CampaignId"" = " + request.CampaignId.ToString() +
                $@" AND ""CreatedAt"" BETWEEN '{request.StartDate.ToString("yyyy-MM-dd")}' AND '{request.EndDate.AddDays(1).ToString("yyyy-MM-dd")}'
                GROUP BY ""Day"", ""CampaignId"" 
                ORDER BY ""Day"" ";

            var data = await dbContext.AdImpressionReports.FromSqlRaw(sqlCommandText).Select(AdImpressionReportByCampaignIdDto.Projection).ToListAsync();

            var result = new Response<List<AdImpressionReportByCampaignIdDto>>();
            return (data != null)
                ? result.OK(data)
                : result.NotFound();
        }
    }
}
