using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.Campaign.Commands.CreateCampaign
{
    public class CreateCampaignMappingProfile : Profile
    {
        public CreateCampaignMappingProfile()
        {
            CreateMap<CreateCampaignCommand, CampaignEntity>();
        }
    }
}