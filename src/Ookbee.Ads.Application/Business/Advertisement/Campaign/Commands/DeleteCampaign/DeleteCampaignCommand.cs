using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Advertisement.Campaign.Commands.DeleteCampaign
{
    public class DeleteCampaignCommand : IRequest<HttpResult<bool>>
    {
        public long Id { get; set; }

        public DeleteCampaignCommand(long id)
        {
            Id = id;
        }
    }
}
