using AutoMapper;
using Ookbee.Ads.Application.Business.AdNetwork.Campaign.Commands.UpdateCampaign;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.AdNetwork.CampaignCost.Commands.UpdateCampaignCost
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UpdateCampaignCostCommand, UpdateCampaignRequest>();
            CreateMap<UpdateCampaignCostCommand, CampaignEntity>();
            CreateMap<UpdateCampaignCostCommand, CampaignCostEntity>();
        }
    }
}
