using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Ad.Commands.UpdateAd
{
    public class UpdateAdCommand : UpdateAdRequest, IRequest<HttpResult<bool>>
    {
        public long Id { get; set; }

        public UpdateAdCommand(long id, UpdateAdRequest request)
        {
            Id = id;
            AdUnitId = request.AdUnitId;
            CampaignId = request.CampaignId;
            Name = request.Name;
            Description = request.Description;
            Status = request.Status;
            ForegroundColor = request.ForegroundColor;
            BackgroundColor = request.BackgroundColor;
            Analytics = request.Analytics;
            Platforms = request.Platforms;
            AppLink = request.AppLink;
            WebLink = request.WebLink;
        }
    }
}
