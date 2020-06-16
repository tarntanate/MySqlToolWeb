using System;
using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.CampaignImpression.Commands.UpdateCampaignImpression
{
    public class UpdateCampaignImpressionCommand : IRequest<HttpResult<bool>>
    {
        public long Id { get; set; }
        public long AdvertiserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PricingModel { get; set; }
        public decimal Quota { get; set; }

        public UpdateCampaignImpressionCommand()
        {

        }

        public UpdateCampaignImpressionCommand(long id, UpdateCampaignImpressionCommand request)
        {
            Id = id;
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
