using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Ad.Commands.DeleteAd
{
    public class DeleteAdCommand : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public string CampaignId { get; set; }

        public DeleteAdCommand(string campaignId, string id)
        {
            Id = id;
            CampaignId = campaignId;
        }
    }
}
