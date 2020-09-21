using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Advertisement.Ad.Commands.UpdateAd
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
            ForegroundColor = request.ForegroundColor;
            BackgroundColor = request.BackgroundColor;
            Quota = request.Quota;
            StartAt = request.StartAt;
            EndAt = request.EndAt;
            Analytics = request.Analytics;
            Platforms = request.Platforms;
            Status = request.Status;
            AppLink = request.AppLink;
            LinkUrl = request.LinkUrl;
        }
    }
}
