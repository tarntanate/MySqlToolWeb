using System;
using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.CampaignCost.Commands.CreateCampaignCost
{
    public class CreateCampaignCostCommand : IRequest<HttpResult<long>>
    {
        public long AdvertiserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public string PricingModel { get; set; }
        public decimal Budget { get; set; }
        public decimal CostPerUnit { get; set; }

        public CreateCampaignCostCommand()
        {

        }

        public CreateCampaignCostCommand(CreateCampaignCostCommand request)
        {
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
