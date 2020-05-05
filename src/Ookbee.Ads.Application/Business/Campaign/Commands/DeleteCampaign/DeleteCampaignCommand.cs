using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Campaign.Commands.DeleteCampaign
{
    public class DeleteCampaignCommand : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public DeleteCampaignCommand(string id)
        {
            Id = id;
        }
    }
}
