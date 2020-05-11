using MediatR;
using MongoDB.Bson;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.PricingModel.Commands.CreatePricingModel
{
    public class CreatePricingModelCommand : IRequest<HttpResult<string>>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public CreatePricingModelCommand()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }

        public CreatePricingModelCommand(CreatePricingModelCommand request)
        {
            Id = ObjectId.GenerateNewId().ToString();
            Name = request.Name;
            Description = request.Description;
        }
    }
}
