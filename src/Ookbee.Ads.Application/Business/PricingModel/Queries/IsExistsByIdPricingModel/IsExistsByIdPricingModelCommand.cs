using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.PricingModel.Queries.IsExistsByIdPricingModel
{
    public class IsExistsByIdPricingModelCommand : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public IsExistsByIdPricingModelCommand(string id)
        {
            Id = id;
        }
    }
}
