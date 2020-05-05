using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.CampaignItemType.Queries.IsExistsByIdCampaignItemType
{
    public class IsExistsByIdCampaignItemTypeCommand : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public IsExistsByIdCampaignItemTypeCommand(string id)
        {
            Id = id;
        }
    }
}
