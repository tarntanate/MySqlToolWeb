using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.CampaignItemType.Queries.GetByIdCampaignItemType
{
    public class GetByIdCampaignItemTypeCommand : IRequest<HttpResult<CampaignItemTypeDto>>
    {
        public string Id { get; set; }

        public GetByIdCampaignItemTypeCommand(string id)
        {
            Id = id;
        }
    }
}
