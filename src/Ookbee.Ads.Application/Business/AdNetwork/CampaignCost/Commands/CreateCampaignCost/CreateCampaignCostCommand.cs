using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Business.AdNetwork.CampaignCost.Commands.CreateCampaignCost
{
    public class CreateCampaignCostCommand : CreateCampaignCostRequest, IRequest<HttpResult<long>>
    {
        public CreateCampaignCostCommand(CreateCampaignCostRequest request)
        {
            AdvertiserId = request.AdvertiserId;
            Name = request.Name;
            Description = request.Description;
            PricingModel = PricingModel.CPM;
            Budget = request.Budget;
            CostPerUnit = request.CostPerUnit;
            StartDate = request.StartDate;
            EndDate = request.EndDate;
        }
    }
}
