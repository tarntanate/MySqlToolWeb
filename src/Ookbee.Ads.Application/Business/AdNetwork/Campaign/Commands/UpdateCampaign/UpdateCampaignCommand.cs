using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.Campaign.Commands.UpdateCampaign
{
    public class UpdateCampaignCommand : UpdateCampaignRequest, IRequest<HttpResult<bool>>
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
