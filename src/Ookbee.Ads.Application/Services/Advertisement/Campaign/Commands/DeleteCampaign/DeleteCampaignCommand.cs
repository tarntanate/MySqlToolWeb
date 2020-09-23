using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.Campaign.Commands.DeleteCampaign
{
    public class DeleteCampaignCommand : IRequest<Response<bool>>
    {
        public long Id { get; set; }

        public DeleteCampaignCommand(long id)
        {
            Id = id;
        }
    }
}
