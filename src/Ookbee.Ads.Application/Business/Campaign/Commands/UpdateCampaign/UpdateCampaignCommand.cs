using System;
using MediatR;
using Ookbee.Ads.Infrastructure.Enums;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Campaign.Commands.UpdateCampaign
{
    public class UpdateCampaignCommand : IRequest<HttpResult<bool>>
    {
        public long Id { get; set; }
        public long AdvertiserId { get; set; }
        public PricingModel PricingModel { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }

        public UpdateCampaignCommand()
        {

        }

        public UpdateCampaignCommand(long id, UpdateCampaignCommand request)
        {
            Id = id;
            AdvertiserId = request.AdvertiserId;
            PricingModel = request.PricingModel;
            Name = request.Name;
            Description = request.Description;
            StartDate = request.StartDate;
            EndDate = request.EndDate;
        }
    }
}
