using MediatR;
using MongoDB.Bson;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.CampaignPricingModel.Commands.CreateCampaignPricingModel
{
    public class CreateCampaignPricingModelCommand : IRequest<HttpResult<string>>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public CreateCampaignPricingModelCommand()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }

        public CreateCampaignPricingModelCommand(CreateCampaignPricingModelCommand request)
        {
            Id = ObjectId.GenerateNewId().ToString();
            Name = request.Name;
            Description = request.Description;
        }
    }
}
