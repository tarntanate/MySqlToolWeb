using System;
using MediatR;
using Ookbee.Ads.Application.Infrastructure.Enums;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Campaign.Commands.CreateCampaign
{
    public class CreateCampaignCommand : IRequest<HttpResult<long>>
    {
        public long AdvertiserId { get; set; }
        public PricingModel PricingModel { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }

        public CreateCampaignCommand()
        {

        }

        public CreateCampaignCommand(CreateCampaignCommand request)
        {
            AdvertiserId = request.AdvertiserId;
            PricingModel = request.PricingModel;
            Name = request.Name;
            Description = request.Description;
            StartDate = request.StartDate;
            EndDate = request.EndDate;
        }
    }
}
