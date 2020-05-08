using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.CampaignItem.Queries.GetByIdCampaignItem
{
    public class GetByIdCampaignItemCommand : IRequest<HttpResult<CampaignItemDto>>
    {
        public string Id { get; set; }

        public GetByIdCampaignItemCommand(string id)
        {
            Id = id;
        }
    }
}
