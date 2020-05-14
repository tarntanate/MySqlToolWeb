using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Campaign.Queries.IsExistsByIdCampaign
{
    public class IsExistsByIdCampaignCommand : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public IsExistsByIdCampaignCommand(string id)
        {
            Id = id;
        }
    }
}
