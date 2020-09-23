using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.Campaign.Commands.CreateCampaign
{
    public class CreateCampaignCommand : CreateCampaignRequest, IRequest<Response<long>>
    {
        public CreateCampaignCommand(CreateCampaignRequest request)
        {
            AdvertiserId = request.AdvertiserId;
            Name = request.Name;
            Description = request.Description;
        }
    }
}
