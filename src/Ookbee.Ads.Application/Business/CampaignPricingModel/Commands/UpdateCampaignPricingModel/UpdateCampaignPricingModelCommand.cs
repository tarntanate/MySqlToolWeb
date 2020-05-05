using MediatR;
using MongoDB.Bson;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.CampaignPricingModel.Commands.UpdateCampaignPricingModel
{
    public class UpdateCampaignPricingModelCommand : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public UpdateCampaignPricingModelCommand()
        {
            Id = string.Empty;
        }

        public UpdateCampaignPricingModelCommand(string id, UpdateCampaignPricingModelCommand request)
        {
            Id = id;
            Name = request.Name;
            Description = request.Description;
        }
    }
}
