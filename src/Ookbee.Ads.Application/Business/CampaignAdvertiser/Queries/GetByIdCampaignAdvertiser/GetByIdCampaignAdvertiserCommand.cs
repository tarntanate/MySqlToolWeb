using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.CampaignAdvertiser.Queries.GetByIdCampaignAdvertiser
{
    public class GetByIdCampaignAdvertiserCommand : IRequest<HttpResult<CampaignAdvertiserDto>>
    {
        public string Id { get; set; }

        public GetByIdCampaignAdvertiserCommand(string id)
        {
            Id = id;
        }
    }
}
