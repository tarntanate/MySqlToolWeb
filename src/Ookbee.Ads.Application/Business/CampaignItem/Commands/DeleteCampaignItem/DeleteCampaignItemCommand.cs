using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.CampaignItem.Commands.DeleteCampaignItem
{
    public class DeleteCampaignItemCommand : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public DeleteCampaignItemCommand(string id)
        {
            Id = id;
        }
    }
}
