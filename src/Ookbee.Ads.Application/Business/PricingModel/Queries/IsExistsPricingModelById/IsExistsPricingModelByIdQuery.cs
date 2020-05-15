using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.PricingModel.Queries.IsExistsPricingModelById
{
    public class IsExistsPricingModelByIdQuery : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public IsExistsPricingModelByIdQuery(string id)
        {
            Id = id;
        }
    }
}
