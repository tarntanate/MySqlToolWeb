using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Campaign.Queries.GetByIdCampaign
{
    public class GetByIdCampaignCommand : IRequest<HttpResult<CampaignDto>>
    {
        public string Id { get; set; }

        public GetByIdCampaignCommand(string id)
        {
            Id = id;
        }
    }
}
