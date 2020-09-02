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
            ForegroundColor = request.ForegroundColor;
            BackgroundColor = request.BackgroundColor;
            Quota = request.Quota;
            PeriodStartAt = request.PeriodStartAt;
            PeriodEndAt = request.PeriodEndAt;
            Analytics = request.Analytics;
            Platforms = request.Platforms;
            Status = request.Status;
            AppLink = request.AppLink;
            LinkUrl = request.LinkUrl;
        }
    }
}
