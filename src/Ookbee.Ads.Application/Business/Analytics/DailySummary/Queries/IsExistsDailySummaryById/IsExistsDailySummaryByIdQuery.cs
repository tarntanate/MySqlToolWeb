using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Analytics.DailySummary
{
    public class IsExistsDailySummaryByIdQuery : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public IsExistsDailySummaryByIdQuery(string id)
        {
            Id = id;
        }
    }
}
