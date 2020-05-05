using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.CampaignAdvertiser.Queries.IsExistsByIdCampaignAdvertiser
{
    public class IsExistsByIdCampaignAdvertiserCommand : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public IsExistsByIdCampaignAdvertiserCommand(string id)
        {
            Id = id;
        }
    }
}
