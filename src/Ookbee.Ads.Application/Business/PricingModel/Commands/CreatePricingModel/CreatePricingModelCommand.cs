using MediatR;
using MongoDB.Bson;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.PricingModel.Commands.CreatePricingModel
{
    public class CreatePricingModelCommand : IRequest<HttpResult<string>>
    {
        public string Id => ObjectId.GenerateNewId().ToString();
        
        public string Name { get; set; }

        public string Description { get; set; }

        public bool EnabledFlag => true;

        public CreatePricingModelCommand()
        {
            
        }

        public CreatePricingModelCommand(CreatePricingModelCommand request)
        {
            Name = request.Name;
            Description = request.Description;
        }
    }
}
