using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.PricingModel.Queries.GetPricingModelByName
{
    public class GetPricingModelByNameQuery : IRequest<HttpResult<PricingModelDto>>
    {
        public string Name { get; set; }

        public GetPricingModelByNameQuery(string name)
        {
            Name = name;
        }
    }
}
