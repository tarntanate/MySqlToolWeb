using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.CampaignItemAsset.Commands.DeleteCampaignItemAsset
{
    public class DeleteCampaignItemAssetCommand : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public DeleteCampaignItemAssetCommand(string id)
        {
            Id = id;
        }
    }
}
