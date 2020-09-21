using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.Advertisement.Campaign.Commands.UpdateCampaign
{
    public class UpdateCampaignMappingProfile : Profile
    {
        public UpdateCampaignMappingProfile()
        {
            CreateMap<UpdateCampaignCommand, CampaignEntity>();
        }
    }
}
