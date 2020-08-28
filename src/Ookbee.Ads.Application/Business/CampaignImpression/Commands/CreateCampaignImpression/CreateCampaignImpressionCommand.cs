using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Infrastructure.Models;

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
            PricingModel = PricingModel.IMP;
            Quota = request.Quota;
        }
    }
}
