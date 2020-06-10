using System;
using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.CampaignImpression.Commands.CreateCampaignImpression
{
    public class CreateCampaignImpressionCommand : IRequest<HttpResult<long>>
    {
        public long AdvertiserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public string PricingModel { get; set; }
        public decimal Quota { get; set; }

        public CreateCampaignImpressionCommand()
        {

        }

        public CreateCampaignImpressionCommand(CreateCampaignImpressionCommand request)
        {
            AdvertiserId = request.AdvertiserId;
            PricingModel = request.PricingModel;
            Name = request.Name;
            Description = request.Description;
            StartDate = request.StartDate;
            EndDate = request.EndDate;
            Quota = request.Quota;
        }
    }
}
