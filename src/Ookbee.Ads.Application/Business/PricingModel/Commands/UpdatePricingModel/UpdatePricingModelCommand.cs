using MediatR;
using MongoDB.Bson;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.PricingModel.Commands.UpdatePricingModel
{
    public class UpdatePricingModelCommand : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool EnabledFlag { get; set; }

        public UpdatePricingModelCommand()
        {
            Id = string.Empty;
        }

        public UpdatePricingModelCommand(string id, UpdatePricingModelCommand request)
        {
            Id = id;
            Name = request.Name;
            Description = request.Description;
        }
    }
}
