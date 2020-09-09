using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.Campaign.Commands.CreateCampaign
{
    public class CreateCampaignCommand : CreateCampaignRequest, IRequest<HttpResult<long>>
    {
        public CreateCampaignCommand(CreateCampaignRequest request)
        {
            AdvertiserId = request.AdvertiserId;
            Name = request.Name;
            Description = request.Description;
        }
    }
}
