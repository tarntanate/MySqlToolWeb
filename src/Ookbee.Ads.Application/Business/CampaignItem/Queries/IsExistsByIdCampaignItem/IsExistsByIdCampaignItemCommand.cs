using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.CampaignItem.Queries.IsExistsByIdCampaignItem
{
    public class IsExistsByIdCampaignItemCommand : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public IsExistsByIdCampaignItemCommand(string id)
        {
            Id = id;
        }
    }
}
