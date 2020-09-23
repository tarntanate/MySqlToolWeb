using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.Campaign.Commands.UpdateCampaign
{
    public class UpdateCampaignCommand : IRequest<Response<bool>>
    {
        public long Id { get; private set; }
        public long AdvertiserId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public UpdateCampaignCommand(long id, UpdateCampaignRequest request)
        {
            Id = id;
            AdvertiserId = request.AdvertiserId;
            Name = request.Name;
            Description = request.Description;
        }
    }
}
