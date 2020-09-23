using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.Campaign.Commands.UpdateCampaign
{
    public class UpdateCampaignCommand : UpdateCampaignRequest, IRequest<Response<bool>>
    {
        public long Id { get; set; }

        public UpdateCampaignCommand(long id, UpdateCampaignRequest request)
        {
            Id = id;
            AdvertiserId = request.AdvertiserId;
            Name = request.Name;
            Description = request.Description;
        }
    }
}
