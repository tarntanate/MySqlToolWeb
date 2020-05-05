using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.CampaignAdvertiser.Queries.IsExistsByNameCampaignAdvertiser
{
    public class IsExistsByNameCampaignAdvertiserCommand : IRequest<HttpResult<bool>>
    {
        public string Name { get; set; }

        public IsExistsByNameCampaignAdvertiserCommand(string name)
        {
            Name = name;
        }
    }
}
