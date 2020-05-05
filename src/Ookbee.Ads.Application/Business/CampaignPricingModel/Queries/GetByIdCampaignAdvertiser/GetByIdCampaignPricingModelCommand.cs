using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.CampaignPricingModel.Queries.GetByIdCampaignPricingModel
{
    public class GetByIdCampaignPricingModelCommand : IRequest<HttpResult<CampaignPricingModelDto>>
    {
        public string Id { get; set; }

        public GetByIdCampaignPricingModelCommand(string id)
        {
            Id = id;
        }
    }
}
