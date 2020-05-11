using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.PricingModel.Queries.GetByIdPricingModel
{
    public class GetByIdPricingModelCommand : IRequest<HttpResult<PricingModelDto>>
    {
        public string Id { get; set; }

        public GetByIdPricingModelCommand(string id)
        {
            Id = id;
        }
    }
}
