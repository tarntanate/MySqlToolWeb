using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Ad.Commands.CreateAd
{
    public class CreateAdCommand : CreateAdRequest, IRequest<HttpResult<long>>
    {
        public CreateAdCommand(CreateAdRequest request)
        {
            AdUnitId = request.AdUnitId;
            CampaignId = request.CampaignId;
            Name = request.Name;
            Description = request.Description;
            Status = request.Status;
            ForegroundColor = request.ForegroundColor;
            BackgroundColor = request.BackgroundColor;
            Analytics = request.Analytics;
            AppLink = request.AppLink;
            WebLink = request.WebLink;
            Platforms = request.Platforms;
        }
    }
}
