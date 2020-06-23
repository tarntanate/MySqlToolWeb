using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.CampaignImpression.Commands.CreateCampaignImpression
{
    public class CreateCampaignImpressionCommand : CreateCampaignImpressionRequest, IRequest<HttpResult<long>>
    {
        public CreateCampaignImpressionCommand(CreateCampaignImpressionRequest request)
        {
            AdvertiserId = request.AdvertiserId;
            Name = request.Name;
            Description = request.Description;
            StartDate = request.StartDate;
            EndDate = request.EndDate;
            PricingModel = request.PricingModel;
            Quota = request.Quota;
        }
    }
}
