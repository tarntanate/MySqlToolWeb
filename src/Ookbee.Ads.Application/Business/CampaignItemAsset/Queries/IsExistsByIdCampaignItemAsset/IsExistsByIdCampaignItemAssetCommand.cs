using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.CampaignItemAsset.Queries.IsExistsByIdCampaignItemAsset
{
    public class IsExistsByIdCampaignItemAssetCommand : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public IsExistsByIdCampaignItemAssetCommand(string id)
        {
            Id = id;
        }
    }
}
