using MediatR;
using Microsoft.EntityFrameworkCore;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Persistence.EFCore.TimescaleDb;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Report.AdGroupReport.Queries.GetAdImpressionPlatformReportByCampaignId
{
    public class GetAdImpressionReportByCampaignIdHandler : IRequestHandler<GetAdImpressionPlatformReportByCampaignIdQuery, Response<List<PlatformReportDto>>>
    {
        // private TimescaleDbRepository<AdGroupReportDto> AdGroupReportDbRepo { get; }
        private TimescaleDbContext dbContext { get; }

        public GetAdImpressionReportByCampaignIdHandler(TimescaleDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Response<List<PlatformReportDto>>> Handle(GetAdImpressionPlatformReportByCampaignIdQuery request, CancellationToken cancellationToken)
        {
            // var item = new List<AdGroupReportDto>();
            var sqlCommandText = $@"SELECT ""PlatformId"", COUNT(*) as ""Total""
                FROM public.""AdImpressionLog""
                WHERE ""CampaignId"" = " + request.CampaignId.ToString() + @"
                GROUP BY ""PlatformId""";

            var data = await dbContext.PlatformReports.FromSqlRaw(sqlCommandText).Select(PlatformReportDto.Projection).ToListAsync();

            var result = new Response<List<PlatformReportDto>>();
            return (data != null)
                ? result.OK(data)
                : result.NotFound();
        }
    }
}
