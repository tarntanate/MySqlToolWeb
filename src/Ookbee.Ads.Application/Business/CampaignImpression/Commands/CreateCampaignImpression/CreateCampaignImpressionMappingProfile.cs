using AutoMapper;
using Ookbee.Ads.Application.Business.Campaign.Commands.CreateCampaign;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.CampaignImpression.Commands.CreateCampaignImpression
{
    public class CreateCampaignImpressionMappingProfile : Profile
    {
        public CreateCampaignImpressionMappingProfile()
        {
            CreateMap<CreateCampaignImpressionCommand, CreateCampaignRequest>();
            CreateMap<CreateCampaignImpressionCommand, CampaignEntity>();
            CreateMap<CreateCampaignImpressionCommand, CampaignImpressionEntity>();
        }
    }
}
