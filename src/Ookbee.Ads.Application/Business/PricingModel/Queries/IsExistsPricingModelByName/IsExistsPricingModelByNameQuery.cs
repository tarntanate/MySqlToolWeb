using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.PricingModel.Queries.IsExistsPricingModelByName
{
    public class IsExistsPricingModelByNameQuery : IRequest<HttpResult<bool>>
    {
        public string Name { get; set; }

        public IsExistsPricingModelByNameQuery(string name)
        {
            Name = name;
        }
    }
}
