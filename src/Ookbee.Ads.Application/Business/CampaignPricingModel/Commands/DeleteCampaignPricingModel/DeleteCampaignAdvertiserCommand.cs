using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.CampaignPricingModel.Commands.DeleteCampaignPricingModel
{
    public class DeleteCampaignPricingModelCommand : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public DeleteCampaignPricingModelCommand(string id)
        {
            Id = id;
        }
    }
}
