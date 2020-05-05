using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.CampaignAdvertiser.Commands.DeleteCampaignAdvertiser
{
    public class DeleteCampaignAdvertiserCommand : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public DeleteCampaignAdvertiserCommand(string id)
        {
            Id = id;
        }
    }
}
