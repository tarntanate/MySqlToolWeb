using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Analytics.DailySummary
{
    public class UpdateDailySummaryCommand : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public bool EnabledFlag => true;

        public UpdateDailySummaryCommand()
        {

        }

        public UpdateDailySummaryCommand(string id, UpdateDailySummaryCommand request)
        {
            Id = id;
            Name = request.Name;
            Description = request.Description;
            ImageUrl = request.ImageUrl;
        }
    }
}
