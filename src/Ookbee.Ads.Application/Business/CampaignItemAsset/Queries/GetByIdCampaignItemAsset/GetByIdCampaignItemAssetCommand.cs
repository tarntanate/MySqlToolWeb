using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.CampaignItemAsset.Queries.GetByIdCampaignItemAsset
{
    public class GetByIdCampaignItemAssetCommand : IRequest<HttpResult<CampaignItemAssetDto>>
    {
        public string Id { get; set; }

        public GetByIdCampaignItemAssetCommand(string id)
        {
            Id = id;
        }
    }
}
