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
            Platforms = request.Platforms;
            ForegroundColor = request.ForegroundColor;
            BackgroundColor = request.BackgroundColor;
            Analytics = request.Analytics;
            AppLink = request.AppLink;
            WebLink = request.WebLink;
        }
    }
}
