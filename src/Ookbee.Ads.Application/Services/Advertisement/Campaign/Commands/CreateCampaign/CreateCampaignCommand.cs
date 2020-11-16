using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.Campaign.Commands.CreateCampaign
{
    public class CreateCampaignCommand : IRequest<Response<long>>
    {
        public long AdvertiserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public CreateCampaignCommand(CreateCampaignRequest request)
        {
            AdvertiserId = request.AdvertiserId;
            Name = request.Name;
            Description = request.Description;
        }
    }
}
