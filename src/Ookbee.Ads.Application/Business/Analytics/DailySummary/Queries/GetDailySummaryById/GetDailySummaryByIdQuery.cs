using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Analytics.DailySummary
{
    public class GetDailySummaryByIdQuery : IRequest<HttpResult<DailySummaryDto>>
    {
        public string Id { get; set; }

        public GetDailySummaryByIdQuery(string id)
        {
            Id = id;
        }
    }
}
