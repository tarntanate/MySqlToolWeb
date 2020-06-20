using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.CampaignCost.Commands.UpdateCampaignCost
{
    public class UpdateCampaignCostCommand : UpdateCampaignCostRequest, IRequest<HttpResult<bool>>
    {
        public long Id { get; set; }

        public UpdateCampaignCostCommand(long id, UpdateCampaignCostRequest request)
        {
            Id = id;
            AdvertiserId = request.AdvertiserId;
            Name = request.Name;
            Description = request.Description;
            StartDate = request.StartDate;
            EndDate = request.EndDate;
            PricingModel = request.PricingModel;
            Budget = request.Budget;
            CostPerUnit = request.CostPerUnit;
        }
    }
}
