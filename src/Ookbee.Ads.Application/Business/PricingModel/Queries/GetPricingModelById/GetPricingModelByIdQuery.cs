using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.PricingModel.Queries.GetPricingModelById
{
    public class GetPricingModelByIdQuery : IRequest<HttpResult<PricingModelDto>>
    {
        public string Id { get; set; }

        public GetPricingModelByIdQuery(string id)
        {
            Id = id;
        }
    }
}
