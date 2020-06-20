using AutoMapper;
using Ookbee.Ads.Application.Business.Campaign.Commands.UpdateCampaign;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.CampaignCost.Commands.UpdateCampaignCost
{
    public class UpdateCampaignCostMappingProfile : Profile
    {
        public UpdateCampaignCostMappingProfile()
        {
            CreateMap<UpdateCampaignCostCommand, UpdateCampaignRequest>();
            CreateMap<UpdateCampaignCostCommand, CampaignEntity>();
            CreateMap<UpdateCampaignCostCommand, CampaignCostEntity>();
        }
    }
}
