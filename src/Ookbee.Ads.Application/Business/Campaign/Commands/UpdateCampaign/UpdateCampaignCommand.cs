using System;
using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Campaign.Commands.UpdateCampaign
{
    public class UpdateCampaignCommand : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public string AdvertiserId { get; set; }

        public string PricingModelId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public decimal Budget { get; set; }

        public int LimitViewTotal { get; set; }

        public int LimitViewPerPerson { get; set; }

        public TimeSpan LimitViewResetAfter { get; set; }

        public int PricingClick { get; set; }

        public int PricingImpressions { get; set; }

        public decimal PricingRate { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool EnabledFlag => true;

        public UpdateCampaignCommand()
        {

        }

        public UpdateCampaignCommand(string id, UpdateCampaignCommand request)
        {
            Id = id;
            AdvertiserId = request.AdvertiserId;
            PricingModelId = request.PricingModelId;
            Name = request.Name;
            Description = request.Description;
            ImageUrl = request.ImageUrl;
            Budget = request.Budget;
            LimitViewTotal = request.LimitViewTotal;
            LimitViewPerPerson = request.LimitViewPerPerson;
            LimitViewResetAfter = request.LimitViewResetAfter;
            PricingClick = request.PricingClick;
            PricingImpressions = request.PricingImpressions;
            PricingRate = request.PricingRate;
            StartDate = request.StartDate;
            EndDate = request.EndDate;
        }
    }
}
