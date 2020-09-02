using AutoMapper;
using Ookbee.Ads.Application.Business.AdNetwork.Campaign.Commands.UpdateCampaign;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.AdNetwork.CampaignImpression.Commands.UpdateCampaignImpression
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UpdateCampaignImpressionCommand, UpdateCampaignRequest>();
            CreateMap<UpdateCampaignImpressionCommand, CampaignEntity>();
            CreateMap<UpdateCampaignImpressionCommand, CampaignImpressionEntity>();
        }
    }
}
