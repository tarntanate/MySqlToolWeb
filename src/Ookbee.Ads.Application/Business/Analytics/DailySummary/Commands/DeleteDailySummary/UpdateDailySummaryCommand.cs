using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Analytics.DailySummary
{
    public class DeleteDailySummaryCommand : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public DeleteDailySummaryCommand(string id)
        {
            Id = id;
        }
    }
}
