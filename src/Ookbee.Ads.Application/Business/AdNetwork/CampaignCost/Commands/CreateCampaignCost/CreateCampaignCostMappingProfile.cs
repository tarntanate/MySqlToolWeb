using AutoMapper;
using Ookbee.Ads.Application.Business.AdNetwork.Campaign.Commands.CreateCampaign;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.AdNetwork.CampaignCost.Commands.CreateCampaignCost
{
    public class CreateCampaignCostMappingProfile : Profile
    {
        public CreateCampaignCostMappingProfile()
        {
            CreateMap<CreateCampaignCostCommand, CreateCampaignRequest>();
            CreateMap<CreateCampaignCostCommand, CampaignEntity>();
            CreateMap<CreateCampaignCostCommand, CampaignCostEntity>();
        }
    }
}