using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.CampaignImpression.Commands.UpdateCampaignImpression
{
    public class UpdateCampaignImpressionCommand : UpdateCampaignImpressionRequest, IRequest<HttpResult<bool>>
    {
        public long Id { get; set; }

        public UpdateCampaignImpressionCommand(long id, UpdateCampaignImpressionRequest request)
        {
            Id = id;
            AdvertiserId = request.AdvertiserId;
            Name = request.Name;
            Description = request.Description;
            StartDate = request.StartDate;
            EndDate = request.EndDate;
            Quota = request.Quota;
        }
    }
}
