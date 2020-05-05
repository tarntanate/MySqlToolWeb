using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.CampaignPricingModel.Queries.IsExistsByIdCampaignPricingModel
{
    public class IsExistsByIdCampaignPricingModelCommand : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public IsExistsByIdCampaignPricingModelCommand(string id)
        {
            Id = id;
        }
    }
}
