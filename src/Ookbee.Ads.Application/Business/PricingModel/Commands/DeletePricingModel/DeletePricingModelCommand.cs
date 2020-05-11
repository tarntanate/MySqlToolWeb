using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.PricingModel.Commands.DeletePricingModel
{
    public class DeletePricingModelCommand : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public DeletePricingModelCommand(string id)
        {
            Id = id;
        }
    }
}
