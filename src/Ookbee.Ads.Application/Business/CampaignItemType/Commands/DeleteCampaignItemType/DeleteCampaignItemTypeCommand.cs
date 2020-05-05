using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.CampaignItemType.Commands.DeleteCampaignItemType
{
    public class DeleteCampaignItemTypeCommand : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public DeleteCampaignItemTypeCommand(string id)
        {
            Id = id;
        }
    }
}
