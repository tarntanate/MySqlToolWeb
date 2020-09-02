using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.CampaignCost.Commands.DeleteCampaignCost
{
    public class DeleteCampaignCostCommand : IRequest<HttpResult<bool>>
    {
        public long Id { get; set; }

        public DeleteCampaignCostCommand(long id)
        {
            Id = id;
        }
    }
}
