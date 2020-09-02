using AutoMapper;
using Ookbee.Ads.Application.Business.AdNetwork.Campaign.Commands.CreateCampaign;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.AdNetwork.CampaignImpression.Commands.CreateCampaignImpression
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCampaignImpressionCommand, CreateCampaignRequest>();
            CreateMap<CreateCampaignImpressionCommand, CampaignEntity>();
            CreateMap<CreateCampaignImpressionCommand, CampaignImpressionEntity>();
        }
    }
}
