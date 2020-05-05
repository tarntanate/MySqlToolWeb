using System;
using MediatR;
using MongoDB.Bson;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Campaign.Commands.CreateCampaign
{
    public class CreateCampaignCommand : IRequest<HttpResult<string>>
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

        public bool IsExpire { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public CreateCampaignCommand()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }

        public CreateCampaignCommand(CreateCampaignCommand request)
        {
            Id = request.Id;
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
            IsExpire = request.IsExpire;
            StartDate = request.StartDate;
            EndDate = request.EndDate;
        }

        public CreateCampaignCommand(string id, string advertiserId, string pricingModelId, string name, string description, string imageUrl, decimal budget, int limitViewTotal, int limitViewPerPerson, TimeSpan limitViewResetAfter, int pricingClick, int pricingImpressions, decimal pricingRate, bool isExpire, DateTime startDate, DateTime endDate)
        {
            Id = id;
            AdvertiserId = advertiserId;
            PricingModelId = pricingModelId;
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
            Budget = budget;
            LimitViewTotal = limitViewTotal;
            LimitViewPerPerson = limitViewPerPerson;
            LimitViewResetAfter = limitViewResetAfter;
            PricingClick = pricingClick;
            PricingImpressions = pricingImpressions;
            PricingRate = pricingRate;
            IsExpire = isExpire;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
