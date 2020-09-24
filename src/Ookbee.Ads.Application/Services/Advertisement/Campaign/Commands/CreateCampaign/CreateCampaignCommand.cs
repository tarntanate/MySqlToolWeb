using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.Campaign.Commands.CreateCampaign
{
    public class CreateCampaignCommand : IRequest<Response<long>>
    {
        public long AdvertiserId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public CreateCampaignCommand(CreateCampaignRequest request)
        {
            AdvertiserId = request.AdvertiserId;
            Name = request.Name;
            Description = request.Description;
        }
    }
}
